import {
  ApiException,
  ProductJson,
  ResponseCategory,
  ResponseNestedCategory,
  ResponseProduct,
  ResponseShop
} from "../_actions";
import {
  ShopApi
} from "./";
import { CategoryApi, ProductApi } from "./Helpers/WebApis";
export const UserService = {
  GetAllShops,
  GetAllProductInShop,
  GetProductById,
  AddShopByUrl,
  GetProductByShopIdAndPage,
  GetAllCategories,
  GetAllCategoriesByPage,
  GetSubCategories
};

async function GetAllShops(): Promise<ResponseShop[]> {

  return ShopApi().getShops().then((shopResponse) => {
    return shopResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetAllCategories(): Promise<ResponseCategory[]> {

  return CategoryApi().getCategories().then((categoryResponse) => {
    return categoryResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetAllCategoriesByPage(page:number,rows:number): Promise<ResponseCategory[]> {

  return CategoryApi().getPagedCategories(page,rows).then((categoryResponse) => {
    return categoryResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetSubCategories(): Promise<ResponseNestedCategory> {

  return CategoryApi().getSubCategories().then((categoryResponse) => {
    return categoryResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}

async function GetAllProductInShop(id:number): Promise<ResponseProduct[]> {

  return ProductApi().getProductsByShopId(id).then((shopResponse) => {
    return shopResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetProductById(id:number): Promise<ProductJson> {

  return ProductApi().getFullProductsById(id).then((productResponse) => {
    return productResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetProductByShopIdAndPage(id:number,page:number,rows:number): Promise<ResponseProduct[]> {

  return ProductApi().getPagedProductsByShopId(id,page,rows).then((productResponse) => {
    return productResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function AddShopByUrl(url:string): Promise<ResponseShop> {

  return ShopApi().addShopByUrl(url).then((shopResponse) => {
    return shopResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}

async function handleExeption(error: ApiException) {

  const exeptionRefuse = JSON.parse(error.response);
  return Promise.reject(exeptionRefuse.message);

}

