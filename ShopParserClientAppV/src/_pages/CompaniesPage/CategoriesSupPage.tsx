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
import { useParams, useRouteMatch, Link } from "react-router-dom";
import { IResponseCategory, UserActions } from "../../_actions";
import { BootstrapInput } from "../../_components";

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

interface CategoriesSupPageProps {
  categorySelectIds: number[]| undefined;
  nestedCategoryList: IResponseCategory[][] | undefined;
  setCategorySelectIds: React.Dispatch<React.SetStateAction<number[]>>;
  setNestedCategoryList: React.Dispatch<
    React.SetStateAction<IResponseCategory[][] | undefined>
  >;
}

export const CategoriesSupPage: React.FC<CategoriesSupPageProps> = ({
  categorySelectIds,
  nestedCategoryList,
  setCategorySelectIds,
  setNestedCategoryList,
}) => {
  const classes = useStyles();

  const { companyId } = useParams<Record<string, string | undefined>>();

  const [nestedCategoryListIsLoading, setNestedCategoryListIsLoading] =
    useState<boolean>();

  useEffect(() => {
    let isMounted = true;
    if (isMounted) {
      setNestedCategoryListIsLoading(true);

      UserActions.GetCategoryByParentIdAndCompanyId(1, Number(companyId)
      ).then((categoryList) => {
        if (isMounted) {
          if (categoryList) {
            setNestedCategoryList(new Array(categoryList));
           }
        //   //console.log(categoryList);
           setNestedCategoryListIsLoading(false);
         }
      });
    }
      

   
    return () => {
      isMounted = false;
    }; // use effect cleanup to set flag false, if unmounted
  }, [companyId, setNestedCategoryList]);

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

      UserActions.GetCategoryByParentIdAndCompanyId(
        categoryId,
        Number(companyId)
      ).then((categoryList) => {
        setNestedCategoryListIsLoading(false);

        if (categoryList !== undefined) {
          newCategoryList.splice(itemId + 1, newCategoryList.length - 1);

          newCategoryList[itemId + 1] = categoryList;
          setNestedCategoryList(newCategoryList);
        }
      });
    }
  };

  const { url } = useRouteMatch();

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
    <Grid item xs container direction="row">
      <Grid item xs>
        {!categorySelectIds?<></>:nestedCategoryList?.map((categoryList, index) => (
          <FormControl className={classes.margin1} key={index}>
            <Select
              input={<BootstrapInput />}
              value={categorySelectIds[index]?categorySelectIds[index]:''}
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
                  value={category.id}
                  key={i}
                  onClick={() => handleCategoryChoose(category.id, index)}
                >
                  <Typography variant="h6">{category.name}</Typography>
                </MenuItem>
              ))}
            </Select>
            <Link
              to={`${url}/${"Category"}`}
              className={`${classes.linkItem} ${classes.divPointer}`}
            >
              <Typography variant="h6" gutterBottom>
                {"Link"}
              </Typography>
            </Link>
          </FormControl>
        ))}
      </Grid>
    </Grid>
  );
};
