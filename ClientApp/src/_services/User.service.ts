import {
  ApiException, ProductJson, ResponseProduct, ResponseShop,
} from "../_actions";
import {
  ShopApi
} from "./";
import { ProductApi } from "./Helpers/WebApis";
export const UserService = {
  GetAllShops,
  GetAllProductInShop,
  GetProductById
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
async function handleExeption(error: ApiException) {

  const exeptionRefuse = JSON.parse(error.response);
  return Promise.reject(exeptionRefuse.message);

}

