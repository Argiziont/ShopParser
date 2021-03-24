import {
  ApiException, ResponseShop,
} from "../_actions";
import {
  ShopApi
} from "./";
export const UserService = {
  GetAllShops,
};

async function GetAllShops(): Promise<ResponseShop[]> {

  return ShopApi().getShops().then((shopResponse) => {
    console.log("Shops imported successgully");
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

