import {
  UserService
} from "../_services";
import { ResponseShop } from "./ClientActions";

export const UserActions = {
  GetAllShops,
  
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