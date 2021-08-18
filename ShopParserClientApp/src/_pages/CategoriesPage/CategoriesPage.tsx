import {
  FormControl,
  Grid,
  MenuItem,
  Select,
  Typography,
  makeStyles,
  CircularProgress,
} from "@material-ui/core";
import React, { useEffect, useState } from "react";
import { Route, Switch, useHistory, useRouteMatch } from "react-router-dom";
//import { useLazyQuery } from '@apollo/client';


import { IResponseCategory, UserActions } from "../../_actions";
import { BootstrapInput } from "../../_components";
import { ProductSubPage } from "./ProductSubPage";

const useStyles = makeStyles((theme) => ({
  divPointer: {
    cursor: "pointer",
  },
  linkItem: {
    textDecoration: "none",
  },
  margin1: {
    margin: theme.spacing(1),
    divPointer: {
      cursor: "pointer",
    },
    linkItem: {
      textDecoration: "none",
    },
  },
}));

interface CategoriesPageProps {
  categorySelectIds: number[] | undefined;
  nestedCategoryList: IResponseCategory[][] | undefined;
  setCategorySelectIds: React.Dispatch<React.SetStateAction<number[]>>;
  setNestedCategoryList: React.Dispatch<
    React.SetStateAction<IResponseCategory[][] | undefined>
  >;
}

export const CategoriesPage: React.FC<CategoriesPageProps> = ({
  categorySelectIds,
  nestedCategoryList,
  setCategorySelectIds,
  setNestedCategoryList,
}) => {
  //const [getCategoryByParentId, { loading: categoryLoading, data: categoryData }] = useLazyQuery<ResponseCompany[]>(GET_CATEGORIES_BY_PARENT_ID);


  const classes = useStyles();

  const history = useHistory();
  const { url, path } = useRouteMatch();

  const [nestedCategoryListIsLoading, setNestedCategoryListIsLoading] =
    useState<boolean>();

  useEffect(() => {
    let isMounted = true;
    if (isMounted) {
      setNestedCategoryListIsLoading(true);
      // if (isMounted) {
      //   getCategoryByParentId({ variables: { categoryId: 'bulldog' } })
      // }
      UserActions.GetCategoryByParentId(1).then((categoryList) => {
        if (isMounted) {
          if (categoryList) {
            if (categoryList.length > 0) {
              setNestedCategoryList(new Array(categoryList));

              const newIdsList: number[] = [];
              if (categoryList[0].id) newIdsList[0] = categoryList[0].id;
              setCategorySelectIds(newIdsList);
            } else {
              setNestedCategoryList([]);
              setCategorySelectIds([]);
            }
          }
          setNestedCategoryListIsLoading(false);
        }
      });
    }

    return () => {
      isMounted = false;
    }; // use effect cleanup to set flag false, if unmounted
  }, [setCategorySelectIds, setNestedCategoryList]);

  const handleCategoryChoose = (
    categoryId: number | undefined,
    itemId: number | undefined
  ) => {
    if (
      nestedCategoryList !== undefined &&
      itemId !== undefined &&
      categoryId !== undefined
    ) {
      const newCategoryList = [...nestedCategoryList];

      UserActions.GetCategoryByParentId(categoryId).then((categoryList) => {
        setNestedCategoryListIsLoading(false);

        if (categoryList !== undefined) {
          if (categoryList.length > 0) {
            newCategoryList.splice(itemId + 1, newCategoryList.length - 1);

            newCategoryList[itemId + 1] = categoryList;
            setNestedCategoryList(newCategoryList);

            if (categorySelectIds) {
              const newIdsList = [...categorySelectIds];

              if (categoryList[0].id)
                newIdsList[itemId + 1] = categoryList[0].id;

              setCategorySelectIds(newIdsList);
            }
          }
          history.push(`${url}/${categoryId}`);
        }
      });
    }
  };

  const handleSelectValueChange = (
    event: React.ChangeEvent<{ value: unknown }>,
    id: number
  ) => {
    if (categorySelectIds) {
      const newIdsList = [...categorySelectIds];
      newIdsList.splice(id + 1, newIdsList.length - 1);
      newIdsList[id] = event.target.value as number;
      setCategorySelectIds(newIdsList);
    }
  };

  return nestedCategoryListIsLoading ? (
    <Grid item>
      <CircularProgress color="inherit" />
    </Grid>
  ) : (
    <>
      <Grid item xs container direction="column">
        <Grid item xs>
          {!categorySelectIds ? (
            <></>
          ) : (
            nestedCategoryList?.map((categoryList, index) => (
              <FormControl className={classes.margin1} key={index}>
                <Select
                  input={<BootstrapInput />}
                  value={
                    categorySelectIds[index] ? categorySelectIds[index] : ""
                  }
                  onChange={(event) => {
                    handleSelectValueChange(event, index);
                  }}
                  MenuProps={{
                    anchorOrigin: {
                      vertical: "bottom",
                      horizontal: "left",
                    },
                    getContentAnchorEl: null,
                  }}
                >
                  {categoryList?.map((category, i) => (
                    <MenuItem
                      key={i}
                      value={category.id}
                      onClick={() => handleCategoryChoose(category.id, index)}
                    >
                      <Typography variant="h6">{category.name}</Typography>
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            ))
          )}
        </Grid>
        <Grid item>
        <Switch>
            <Route path={`${path}/:categoryId`}>
              <ProductSubPage />
            </Route>
            </Switch>
        </Grid>
        </Grid>
        
    </>
  );
};
