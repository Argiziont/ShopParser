import {
  ApiException,
  ProductJson,
  ResponseCategory,
  ResponseNestedCategory,
  ResponseCompany
} from "../_actions";
import {
  CompanyApi
} from "./";
import { CategoryApi, ProductApi } from "./Helpers/WebApis";
export const UserService = {
  GetAllCompanies, //Comanies get
  GetByParentIdAndCompanyId,//Category get categoryId id 
  GetByParentId,//Category get categoryId 
  GetProductByPage,//Products get Page
  GetProductById, //Products get id
  GetProductByCategoryIdAndPage,//Products get categoryId page 
  GetProductByCategoryIdAndCompanyIdAndPage,//Products get categoryId companyId Page
  GetProductsCountByCategoryIdAndCompanyId, //Productscount get categoryId companyId
  GetProductsCountByCategoryId,//Productscount get categoryId
  GetProductsCount//Productscount get
};

async function GetAllCompanies(): Promise<ResponseCompany[]> {

  return CompanyApi().getAll().then((companyResponse) => {
    return companyResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetByParentIdAndCompanyId(id: number, companyId: number): Promise<ResponseCategory[]> {

  return CategoryApi().getNestedByParentIdAndCompanyId(id, companyId).then((categoryResponse) => {
    return categoryResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetByParentId(id: number): Promise<ResponseCategory[]> {

  return CategoryApi().getNestedByParentId(id).then((categoryResponse) => {
    return categoryResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetProductById(id: number): Promise<ProductJson> {

  return ProductApi().getFullProductsById(id).then((productResponse) => {
    return productResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetProductByCategoryIdAndPage(id: number, page: number, rows: number): Promise<ResponseNestedCategory[]> {

  return ProductApi().getPagedProductsByCategoryId(id, page, rows).then((productResponse) => {
    return productResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetProductByCategoryIdAndCompanyIdAndPage(categoryId: number, companyId: number, page: number, rows: number): Promise<ResponseNestedCategory[]> {

  return ProductApi().getPagedProductsByCategoryIdAndCompanyId(categoryId,companyId, page, rows).then((productResponse) => {
    return productResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetProductByPage(page: number, rows: number): Promise<ResponseNestedCategory[]> {

  return ProductApi().getPagedProducts(page, rows).then((productResponse) => {
    return productResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetProductsCountByCategoryIdAndCompanyId(categoryId: number, companyId: number): Promise<number> {

  return CategoryApi().getProductCountByCategoryIdAndCompanyId(categoryId,companyId).then((count) => {
    return count;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetProductsCountByCategoryId(categoryId: number, ): Promise<number> {

  return CategoryApi().getProductCountByCategoryId(categoryId).then((count) => {
    return count;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetProductsCount(): Promise<number> {

  return ProductApi().getTotalProductCount().then((count) => {
    return count;
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

