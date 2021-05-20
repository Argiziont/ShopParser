import {
  CompanyClient,
  ProductClient,
  CategoryClient
} from "../../_actions";

import {
  ApiUrl,
} from "..";

export const CompanyApi = (): CompanyClient => {
  return new CompanyClient(ApiUrl);
};
export const ProductApi = (): ProductClient => {
  return new ProductClient(ApiUrl);
};
export const CategoryApi = (): CategoryClient => {
  return new CategoryClient(ApiUrl);
};
