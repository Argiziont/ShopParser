import {
  WebScraperClient,
} from "../../_actions";

import {
  ApiUrl,
} from "..";

export const WebApi = (): WebScraperClient => {
  return new WebScraperClient(ApiUrl);
};
