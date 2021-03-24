import {
  ShopClient
} from "../../_actions";

import {
  ApiUrl,
} from "..";

export const WebApi = (): ShopClient => {
  return new ShopClient(ApiUrl);
};
