import {
  UserService
} from "../_services";
import { IResponseShop, IProductJson } from "./ClientActions";

export const UserActions = {
  GetAllShops,
  GetAllProductInShop,
  GetProductById,
  AddShopByUrl,
  GetProductByIdAndPage
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
async function GetProductByIdAndPage(id:number,page:number,rows:number): Promise<IResponseShop[]|undefined> {
  try {
    const response = await  UserService.GetProductByIdAndPage(id,page,rows);
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
async function AddShopByUrl(url: string): Promise<IResponseShop | undefined> {
  try {
    const response = await  UserService.AddShopByUrl(url);
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}