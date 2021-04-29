import {
  UserService
} from "../_services";
import { IResponseCompany, IProductJson,IResponseNestedCategory } from "./ClientActions";

export const UserActions = {
  GetAllCompanys,
  GetAllProductInCompany,
  GetProductById,
  AddCompanyByUrl,
  GetProductByCompanyIdAndPage,
  GetSubCategories,
  GetProductByCategoryIdAndPage
};
async function GetAllCompanys(): Promise<IResponseCompany[]|undefined> {
  try {
    const response = await  UserService.GetAllCompanys();
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function GetAllProductInCompany(id:number): Promise<IResponseCompany[]|undefined> {
  try {
    const response = await  UserService.GetAllProductInCompany(id);
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function GetProductByCompanyIdAndPage(id:number,page:number,rows:number): Promise<IResponseCompany[]|undefined> {
  try {
    const response = await  UserService.GetProductByCompanyIdAndPage(id,page,rows);
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function GetProductByCategoryIdAndPage(id:number,page:number,rows:number): Promise<IResponseNestedCategory[]|undefined> {
  try {
    const response = await  UserService.GetProductByCategoryIdAndPage(id,page,rows);
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function GetProductById(id:number): Promise<IProductJson|undefined> {
  try {
    const response = await  UserService.GetProductById(id);
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function GetSubCategories(): Promise<IResponseNestedCategory|undefined> {
  try {
    const response = await  UserService.GetSubCategories();
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}
async function AddCompanyByUrl(url: string): Promise<IResponseCompany | undefined> {
  try {
    const response = await  UserService.AddCompanyByUrl(url);
    return response;
  } 
  catch(error) {
    console.error(error);
  }
}