import {
  FormControl,
  Grid,
  MenuItem,
  Select,
  Typography,
  makeStyles,
  CircularProgress,
} from "@material-ui/core";
import { url } from "node:inspector";
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

export const CategoriesSupPage: React.FC = () => {
  const classes = useStyles();
  const [categorySelectIds, setCategorySelectIds] = useState<number[]>([]);

  const { companyId } = useParams<Record<string, string | undefined>>();
  const [nestedCategoryList, setNestedCategoryList] =
    useState<IResponseCategory[][]>();
  const [nestedCategoryListIsLoading, setNestedCategoryListIsLoading] =
    useState<boolean>();

  useEffect(() => {
    let isMounted = true;
    setNestedCategoryListIsLoading(true);

    UserActions.GetCategoryByParentIdAndCompanyId(
      1,
      Number(companyId?.split(":")[1])
    ).then((categoryList) => {
      if (isMounted && categoryList != undefined) {
        setNestedCategoryList(new Array(categoryList));
        setNestedCategoryListIsLoading(false);
      }
    });
    return () => {
      isMounted = false;
    }; // use effect cleanup to set flag false, if unmounted
  }, [companyId]);

  const handleCategoryChoose = (
    categoryId: number | undefined,
    itemId: number | undefined
  ) => {
    if (
      nestedCategoryList != undefined &&
      itemId != undefined &&
      categoryId != undefined
    ) {
      const newCategoryList = [...nestedCategoryList];

      UserActions.GetCategoryByParentIdAndCompanyId(
        categoryId,
        Number(companyId?.split(":")[1])
      ).then((categoryList) => {
        setNestedCategoryListIsLoading(false);

        if (categoryList != undefined) {
          newCategoryList.splice(itemId + 1, newCategoryList.length - 1);

          newCategoryList[itemId + 1] = categoryList;
          setNestedCategoryList(newCategoryList);
        }
      });
    }
  };

  const { url, path } = useRouteMatch();

  const handleSelectValueChange = (
    event: React.ChangeEvent<{ value: unknown }>,
    id: number
  ) => {
    const newIdsList = [...categorySelectIds];
    newIdsList.splice(id + 1, newIdsList.length - 1);
    newIdsList[id] = event.target.value as number;
    setCategorySelectIds(newIdsList);
  };

  return nestedCategoryListIsLoading ? (
    <Grid item>
      <CircularProgress color="inherit" />
    </Grid>
  ) : (
    <Grid item xs container direction="row">
      <Grid item xs>
        {nestedCategoryList?.map((categoryList, index) => (
          <FormControl className={classes.margin1} key={index}>
            <Select
              input={<BootstrapInput />}
              defaultValue={categoryList[0].id}
              value={categorySelectIds[index]}
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
                // <Link
                //   key={i}
                //   // to={`${url}/Category=${category.id}`}
                //   className={`${classes.linkItem} ${classes.divPointer}`}
                // >
                <MenuItem
                  value={category.id}
                  key={i}
                  onClick={() => handleCategoryChoose(category.id, index)}
                >
                  <Typography variant="h6">{category.name}</Typography>
                </MenuItem>
                // </Link>
              ))}
            </Select>
          </FormControl>
        ))}
      </Grid>
    </Grid>
  );
};
