import {
  UserService
} from "../_services";
import { IResponseCompany, IProductJson,IResponseNestedCategory, IResponseCategory } from "./ClientActions";

export const UserActions = {
  GetAllCompanys,
  GetAllProductInCompany,
  GetProductById,
  AddCompanyByUrl,
  GetProductByCompanyIdAndPage,
  GetSubCategories,
  GetProductByCategoryIdAndPage,
  GetCategoryByParentIdAndCompanyId,
  GetProductByCategoryIdAndCompanyIdAndPage,
  GetProductsCountByCategoryIdAndCompanyId
};
async function GetAllCompanys(): Promise<IResponseCompany[]|undefined> {
  try {
    const response = await  UserService.GetAllCompanys();
    return response;
  } 
  catch(error) {
    //Function returns undefined, don't need to do something
  }
}
async function GetAllProductInCompany(id:number): Promise<IResponseCompany[]|undefined> {
  try {
    const response = await  UserService.GetAllProductInCompany(id);
    return response;
  } 
  catch(error) {
    //Function returns undefined, don't need to do something
  }
}
async function GetProductByCompanyIdAndPage(id:number,page:number,rows:number): Promise<IResponseCompany[]|undefined> {
  try {
    const response = await  UserService.GetProductByCompanyIdAndPage(id,page,rows);
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
async function GetSubCategories(): Promise<IResponseNestedCategory|undefined> {
  try {
    const response = await  UserService.GetSubCategories();
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
async function GetProductsCountByCategoryIdAndCompanyId(id:number, companyId:number): Promise<number|undefined> {
  try {
    const response = await  UserService.GetProductsCountByCategoryIdAndCompanyId(id,companyId);
    return response;
  } 
  catch(error) {
    //Function returns undefined, don't need to do something
  }
}

async function AddCompanyByUrl(url: string): Promise<IResponseCompany | undefined> {
  try {
    const response = await  UserService.AddCompanyByUrl(url);
    return response;
  } 
  catch(error) {
    //Function returns undefined, don't need to do something
  }
}