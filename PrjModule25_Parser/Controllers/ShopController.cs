﻿using AngleSharp;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using PrjModule25_Parser.Models;
using PrjModule25_Parser.Models.JSON_DTO;
using PrjModule25_Parser.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrjModule25_Parser.Controllers.Interfaces;
using PrjModule25_Parser.Models.Helpers;

namespace PrjModule25_Parser.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShopController : ControllerBase,IShopController
    {
        private readonly IBrowsingContext _context;
        private readonly ApplicationDb _dbContext;

        public ShopController(ApplicationDb db)
        {
            var config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(config);
            _dbContext = db;
        }

        [HttpGet]
        [Route("ParseSellerPageProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddProductsListFromSellerAsync(string sellerName= "DAIVVA UKRAINE")
        {
            try
            {
                var shop = _dbContext.Shops.FirstOrDefault(s => s.Name == sellerName);
                if (shop == null)
                {
                    return BadRequest("Database doesn't contains this seller");
                }

                var sellerUrl = _dbContext.Shops.FirstOrDefault(u => u.Name == sellerName)?.Url;
                var sellerPage = await _context.OpenAsync(sellerUrl);

                //Number of pages   
                var pageCount = sellerPage.QuerySelectorAll("button[data-qaid='pages']")
                    .Select(m => int.Parse(m.InnerHtml))
                    .Max();


                //Get all pages for current seller
                var productsLinkList = new List<string>();
                for (var i = 1; i <= pageCount; i++)
                {
                    var page = await _context.OpenAsync(sellerUrl?.Replace(".html", "") + ";" + i + ".html");
                    productsLinkList.AddRange(
                        page.QuerySelectorAll("*[data-qaid='product_link']").ToList().Cast<IHtmlAnchorElement>()
                            .Select(u => u.Href));
                }

                var emptyProducts = productsLinkList.Select(url => new ProductData
                {
                    Url = url,
                    Shop = shop,
                    ProductState = ProductState.Idle
                }).ToList();

                shop.SyncDate = DateTime.Now;

                var jsonSellerDat = new ShopJson
                {
                    ExternalId = shop.ExternalId,
                    Url = shop.Url,
                    SyncDate = shop.SyncDate,
                    Name = shop.Name,
                };

                var generator = new JSchemaGenerator();
                var fullCategorySchema = generator.Generate(typeof(ShopJson)).ToString();
                var fullCategoryJson = JsonConvert.SerializeObject(jsonSellerDat);

                shop.Products = emptyProducts;
                shop.JsonData = fullCategoryJson;
                shop.JsonDataSchema = fullCategorySchema;

                await _dbContext.Products.AddRangeAsync(emptyProducts);
                _dbContext.Entry(shop).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                return Ok(productsLinkList);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("ParseSellerPage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ShopJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> FindDataInsideSellerPageAsync(
            string sellerUrl = "https://prom.ua/c3468819-daivva-ukraine.html")
        {
            try
            {
                var sellerPage = await _context.OpenAsync(sellerUrl);

                //Seller data
                var externalId = sellerUrl.Split("//")[1].Split('/')[1].Split('-').First();
                var sellerName = sellerPage.QuerySelector("span[data-qaid='company_name']")?.InnerHtml ?? "";

                if (_dbContext.Shops.FirstOrDefault(s => s.ExternalId == externalId) != null)
                {
                    return BadRequest("Database already contains this seller");
                }

                var jsonSellerDat = new ShopJson
                {
                    ExternalId = externalId,
                    Url = sellerUrl,
                    SyncDate = DateTime.Now,
                    Name = sellerName
                };
                var generator = new JSchemaGenerator();

                var fullCategorySchema = generator.Generate(typeof(ShopJson)).ToString();
                var fullCategoryJson = JsonConvert.SerializeObject(jsonSellerDat);

                var seller = new ShopData
                {
                    Url = jsonSellerDat.Url,
                    ExternalId = jsonSellerDat.ExternalId,
                    SyncDate = jsonSellerDat.SyncDate,
                    Name = jsonSellerDat.Name,
                    JsonData = fullCategoryJson,
                    JsonDataSchema = fullCategorySchema
                };
                await _dbContext.Shops.AddAsync(seller);
                await _dbContext.SaveChangesAsync();

                return Ok(seller);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}