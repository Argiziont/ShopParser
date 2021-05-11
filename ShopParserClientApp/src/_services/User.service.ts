import {
  ApiException,
  ProductJson,
  ResponseCategory,
  ResponseNestedCategory,
  ResponseProduct,
  ResponseCompany
} from "../_actions";
import {
  CompanyApi
} from "./";
import { CategoryApi, ProductApi } from "./Helpers/WebApis";
export const UserService = {
  GetAllCompanys,
  GetAllProductInCompany,
  GetProductById,
  AddCompanyByUrl,
  GetProductByCompanyIdAndPage,
  GetProductByCategoryIdAndPage,
  GetAllCategories,
  GetAllCategoriesByPage,
  GetSubCategories
};

async function GetAllCompanys(): Promise<ResponseCompany[]> {

  return CompanyApi().getAll().then((companyResponse) => {
    return companyResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetAllCategories(): Promise<ResponseCategory[]> {

  return CategoryApi().getAll().then((categoryResponse) => {
    return categoryResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetAllCategoriesByPage(page:number,rows:number): Promise<ResponseCategory[]> {

  return CategoryApi().getPaged(page,rows).then((categoryResponse) => {
    return categoryResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetSubCategories(): Promise<ResponseNestedCategory> {

  return CategoryApi().getAllNested().then((categoryResponse) => {
    return categoryResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}

async function GetAllProductInCompany(id:number): Promise<ResponseProduct[]> {

  return ProductApi().getProductsByCompanyId(id).then((companyResponse) => {
    return companyResponse;
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
async function GetProductByCompanyIdAndPage(id:number,page:number,rows:number): Promise<ResponseProduct[]> {

  return ProductApi().getPagedProductsByCompanyId(id,page,rows).then((productResponse) => {
    return productResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function GetProductByCategoryIdAndPage(id:number,page:number,rows:number): Promise<ResponseNestedCategory[]> {

  return ProductApi().getPagedProductsByCategoryId(id,page,rows).then((productResponse) => {
    return productResponse;
    //Ok
  }, async (error) => {
    const handledException = await handleExeption(error);
    return Promise.reject(handledException);
  });
}
async function AddCompanyByUrl(url:string): Promise<ResponseCompany> {

  return CompanyApi().addByUrl(url).then((companyResponse) => {
    return companyResponse;
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

