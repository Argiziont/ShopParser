import {
  UserService
} from "../_services";
import { IResponseCompany, IProductJson,IResponseNestedCategory, IResponseCategory } from "./ClientActions";

export const UserActions = {
  GetAllCompanies,
  GetProductById,
  GetProductByCategoryIdAndPage,
  GetCategoryByParentIdAndCompanyId,
  GetCategoryByParentId,
  GetProductByCategoryIdAndCompanyIdAndPage,
  GetProductsCountByCategoryIdAndCompanyId,
  GetProductsCountByCategoryId,
  GetProductByPage,
  GetProductsCount
};
async function GetAllCompanies(): Promise<IResponseCompany[]|undefined> {
  try {
    const response = await  UserService.GetAllCompanies();
    return response;
  } 
  catch(error) {
    //Function returns undefined, don't need to do something
  }
}
async function GetProductByCategoryIdAndPage(id:number,page:number,rows:number): Promise<IResponseNestedCategory[]|undefined> {
  try {
    const response = await  UserService.GetProductByCategoryIdAndPage(id,page,rows);
    return response;
  } 
  catch(error) {
   //Function returns undefined, don't need to do something
  }
}
async function GetProductByCategoryIdAndCompanyIdAndPage(categoryId: number, companyId: number, page: number, rows: number): Promise<IResponseNestedCategory[]|undefined> {
  try {
    const response = await  UserService.GetProductByCategoryIdAndCompanyIdAndPage(categoryId,companyId,page,rows);
    return response;
  } 
  catch(error) {
   //Function returns undefined, don't need to do something
  }
}
async function GetProductById(id:number): Promise<IProductJson|undefined> {
  try {
    const response = await  UserService.GetProductById(id);
    return response;
  } 
  catch(error) {
    //Function returns undefined, don't need to do something
  }
}
async function GetProductByPage(page: number, rows: number): Promise<IResponseNestedCategory[]|undefined> {
  try {
    const response = await  UserService.GetProductByPage(page,rows);
    return response;
  } 
  catch(error) {
    //Function returns undefined, don't need to do something
  }
}
async function GetCategoryByParentIdAndCompanyId(id:number, companyId:number): Promise<IResponseCategory[]|undefined> {
  try {
    const response = await  UserService.GetByParentIdAndCompanyId(id,companyId);
    return response;
  } 
  catch(error) {
    //Function returns undefined, don't need to do something
  }
}
async function GetCategoryByParentId(id:number): Promise<IResponseCategory[]|undefined> {
  try {
    const response = await  UserService.GetByParentId(id);
    return response;
  } 
  catch(error) {
    //Function returns undefined, don't need to do something
  }
}
async function GetProductsCountByCategoryIdAndCompanyId(id:number, companyId:number): Promise<number|undefined> {
  try {
    const response = await  UserService.GetProductsCountByCategoryIdAndCompanyId(id,companyId);
    return response;
  } 
  catch(error) {
    //Function returns undefined, don't need to do something
  }
}
async function GetProductsCountByCategoryId(id:number): Promise<number|undefined> {
  try {
    const response = await  UserService.GetProductsCountByCategoryId(id);
    return response;
  } 
  catch(error) {
    //Function returns undefined, don't need to do something
  }
}
async function GetProductsCount(): Promise<number|undefined> {
  try {
    const response = await  UserService.GetProductsCount();
    return response;
  } 
  catch(error) {
    //Function returns undefined, don't need to do something
  }
}