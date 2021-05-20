import React from 'react';
import { IResponseCategory } from '../_actions';

type CategoryContextProps = { 
  categorySelectIds: number[]| undefined;
  nestedCategoryList: IResponseCategory[][] | undefined;
  setCategorySelectIds: React.Dispatch<React.SetStateAction<number[]>>;
  setNestedCategoryList: React.Dispatch<
    React.SetStateAction<IResponseCategory[][] | undefined>
  >;
};
export const CategoryContext = 
  React.createContext<Partial<CategoryContextProps>>({});