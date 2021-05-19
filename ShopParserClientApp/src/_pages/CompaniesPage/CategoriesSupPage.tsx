import {
  withStyles,
  Theme,
  createStyles,
  InputBase,
  FormControl,
  Grid,
  MenuItem,
  Select,
  Typography,
  makeStyles,
  CircularProgress,
} from "@material-ui/core";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import {
  IResponseCategory,
  ResponseCategory,
  UserActions,
} from "../../_actions";

export const CategoriesSupPage: React.FC = () => {
  const BootstrapInput = withStyles((theme: Theme) =>
    createStyles({
      root: {
        "label + &": {
          marginTop: theme.spacing(3),
        },
      },
      input: {
        borderRadius: 4,
        position: "relative",
        backgroundColor: "#D3D3D3",
        border: "1px solid #ced4da",
        fontSize: 16,
        minWidth: "150px",
        padding: "10px 26px 10px 12px",
        transition: theme.transitions.create(["border-color", "box-shadow"]),
        // Use the system font instead of the default Roboto font.
        fontFamily: [
          "-apple-system",
          "BlinkMacSystemFont",
          '"Segoe UI"',
          "Roboto",
          '"Helvetica Neue"',
          "Arial",
          "sans-serif",
          '"Apple Color Emoji"',
          '"Segoe UI Emoji"',
          '"Segoe UI Symbol"',
        ].join(","),
        "&:focus": {
          borderRadius: 4,
          borderColor: "#80bdff",
          boxShadow: "0 0 0 0.2rem rgba(0,123,255,.25)",
        },
      },
    })
  )(InputBase);
  const useStyles = makeStyles((theme) => ({
    margin1: {
      margin: theme.spacing(1),
    },
  }));
  const classes = useStyles();

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
        console.log(categoryList);
        setNestedCategoryListIsLoading(false);
      }
    });
    return () => {
      isMounted = false;
    }; // use effect cleanup to set flag false, if unmounted
  }, [companyId]);
  return nestedCategoryListIsLoading ? (
    <Grid item>
      <CircularProgress color="inherit" />
    </Grid>
  ) : (
    <Grid item xs container direction="row">
      <Grid item xs>
        {nestedCategoryList?.map((categoryList, i) => (
          <FormControl className={classes.margin1} key={i}>
            <Select
              input={<BootstrapInput />}
              defaultValue={categoryList[0].id}
              MenuProps={{
                anchorOrigin: {
                  vertical: "bottom",
                  horizontal: "left",
                },
                getContentAnchorEl: null,
              }}
            >
              {categoryList?.map((category, i) => (
                <MenuItem key={i} value={category.id}>
                  <Typography variant="h6">{category.name}</Typography>
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        ))}
      </Grid>
    </Grid>
  );
};
//<h3>{companyId?.split(":")[1]}</h3>
