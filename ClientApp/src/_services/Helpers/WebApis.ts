import {
  ShopClient,
  ProductClient
} from "../../_actions";

import {
  ApiUrl,
} from "..";

export const ShopApi = (): ShopClient => {
  return new ShopClient(ApiUrl);
};
export const ProductApi = (): ProductClient => {
  return new ProductClient(ApiUrl);
};
