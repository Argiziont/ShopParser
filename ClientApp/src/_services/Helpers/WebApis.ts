import {
  ShopClient,
  ProductClient,
  CategoryClient
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
export const CategoryApi = (): CategoryClient => {
  return new CategoryClient(ApiUrl);
};
