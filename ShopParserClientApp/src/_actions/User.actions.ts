import {
  UserService
} from "../_services";
import { IResponseShop, IProductJson,IResponseNestedCategory } from "./ClientActions";

export const UserActions = {
  GetAllShops,
  GetAllProductInShop,
  GetProductById,
  AddShopByUrl,
  GetProductByShopIdAndPage,
  GetSubCategories,
  GetProductByCategoryIdAndPage
};
async function GetAllShops(): Promise<IResponseShop[]|undefined> {
  try {
    const response = await  UserService.GetAllShops();
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function GetAllProductInShop(id:number): Promise<IResponseShop[]|undefined> {
  try {
    const response = await  UserService.GetAllProductInShop(id);
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function GetProductByShopIdAndPage(id:number,page:number,rows:number): Promise<IResponseShop[]|undefined> {
  try {
    const response = await  UserService.GetProductByShopIdAndPage(id,page,rows);
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function GetProductByCategoryIdAndPage(id:number,page:number,rows:number): Promise<IResponseNestedCategory[]|undefined> {
  try {
    const response = await  UserService.GetProductByCategoryIdAndPage(id,page,rows);
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function GetProductById(id:number): Promise<IProductJson|undefined> {
  try {
    const response = await  UserService.GetProductById(id);
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function GetSubCategories(): Promise<IResponseNestedCategory|undefined> {
  try {
    const response = await  UserService.GetSubCategories();
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function AddShopByUrl(url: string): Promise<IResponseShop | undefined> {
  try {
    const response = await  UserService.AddShopByUrl(url);
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}