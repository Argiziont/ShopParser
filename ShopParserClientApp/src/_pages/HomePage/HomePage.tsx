import { Grid, makeStyles } from "@material-ui/core";

import React, { useState } from "react";
import { Route, Switch, useRouteMatch } from "react-router-dom";
import { IResponseCategory } from "../../_actions";
import { CategoriesPage } from "../CategoriesPage";
import { CompaniesPage } from "../CompaniesPage";
import { ProductsPage } from "../ProductsPage";
import { HomePageRouting } from "./HomePageRouting";

const useStyles = makeStyles((theme) => ({
  rootBox: {
    marginTop: theme.spacing(5),
    marginBottom: theme.spacing(5),
  },
  urlField: {
    margin: theme.spacing(5),
  },
  menuItem: {
    minWidth: "150px",
    minHeight: "30px",
    background: "#D3D3D3",
    border: 0,
    borderRadius: 16,
    padding: "15px 15px",
    display: "flex",
    justifyContent: "center",
    marginTop: theme.spacing(2),
    textDecoration: "none",
  },
  divPointer: {
    cursor: "pointer",
  },
  typographyLink: {
    color: "black",
  },
}));
export const HomePage: React.FC = () => {
  const classes = useStyles();
  const { path } = useRouteMatch();

  const [categorySelectIds, setCategorySelectIds] = useState<number[]>([]);
  const [nestedCategoryList, setNestedCategoryList] =
    useState<IResponseCategory[][]>();

  return (
    <React.Fragment>
      <Grid
        container
        spacing={3}
        justify="flex-start"
        direction="row"
        alignItems="flex-start"
        className={classes.rootBox}
      >
        <HomePageRouting />
        <Grid
          item
          xs={9}
          spacing={3}
          container
          justify="center"
          direction="row"
        >
          <Grid item container spacing={3} direction="column" justify="center">
            <Grid item container spacing={3}>
              <Switch>
                <Route exact path={path}></Route>
                <Route path={`${path}Company`} component={CompaniesPage} />
                <Route path={`${path}Categories`}>
                  <CategoriesPage
                    categorySelectIds={categorySelectIds}
                    nestedCategoryList={nestedCategoryList}
                    setCategorySelectIds={setCategorySelectIds}
                    setNestedCategoryList={setNestedCategoryList}
                  />
                </Route>
                <Route
                  path={`${path}Products`}
                  component={ProductsPage}
                ></Route>
              </Switch>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};
