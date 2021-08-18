import { gql } from '@apollo/client';

export const GET_CATEGORIES_BY_PARENT_ID = gql`
query Categories($companyId: Int!){
  categories(companyId:$companyId){
    id,
    name,
    href,
    productsCount
  }
}
`;

export const GET_All_COMPANIES = gql`
query{
	companies{
    id,
    externalId,
    productCount,
    name,
    url,
    syncDate
  }
}
`;

export const GET_PRODUCT_INFO_SUB = gql`
subscription{
  subscribeProductGetDate{
    title,
     externalId
  }
}
`;
