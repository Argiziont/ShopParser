import {
  UserService
} from "../_services";
import { ResponseShop } from "./ClientActions";

export const UserActions = {
  GetAllShops,
  GetAllProductInShop
};
async function GetAllShops(): Promise<ResponseShop[]|undefined> {
  try {
    const response = await  UserService.GetAllShops();
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function GetAllProductInShop(id:number): Promise<ResponseShop[]|undefined> {
  try {
    const response = await  UserService.GetAllProductInShop(id);
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}