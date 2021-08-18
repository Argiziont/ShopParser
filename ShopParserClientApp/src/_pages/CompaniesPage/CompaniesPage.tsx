import { NetworkStatus, useQuery, useSubscription } from "@apollo/client";
import {
  CircularProgress,
  Grid,
  makeStyles,
  Typography,
} from "@material-ui/core";

import React, { useState } from "react";
import { Link, Route, Switch, useRouteMatch } from "react-router-dom";
import {
  GET_All_COMPANIES,
  GET_PRODUCT_INFO_SUB,
  IResponseCategory,
} from "../../_actions";
import {
  ResponseCompany,
} from "../../_actions/GraphqlTypes";
import { CategoriesSupPage } from "./CategoriesSupPage";
import { ProductSubPage } from "./ProductSubPage";

const useStyles = makeStyles(() => ({
  divPointer: {
    cursor: "pointer",
  },
  linkItem: {
    textDecoration: "none",
  },
  gridListRoot: {
    flexGrow: 1,
  },
  gridList: {
    flexWrap: "nowrap",
    // Promote the list into his own layer on Chrome. This cost memory but helps keeping high FPS.
    transform: "translateZ(0)",
  },
  typographyLink: {
    color: "black",
  },
  companyItem: {
    maxWidth: "320px",
    // minWidth: "300px",
    background: "#D3D3D3",
    border: 0,
    borderRadius: 16,
    padding: "15px 15px",
  },
}));

interface ResponseCompanyData {
  companies: ResponseCompany[];
};

export const CompaniesPage: React.FC = () => {
  const classes = useStyles();
  const { url, path } = useRouteMatch();

  const { loading:isCompaniesLodaing, error, data:companyList, networkStatus } = useQuery<ResponseCompanyData,ResponseCompany>(GET_All_COMPANIES);
  const [categorySelectIds, setCategorySelectIds] = useState<number[]>([]);
  const [nestedCategoryList, setNestedCategoryList] =
    useState<IResponseCategory[][]>();

  SubToProducts();
  if (networkStatus === NetworkStatus.refetch || isCompaniesLodaing || error) return <CircularProgress color="inherit" />;

  return <React.Fragment>
      <div className={classes.gridListRoot}>
        <Grid container spacing={3}>
        {companyList?.companies?.map((company, i) => (
            <Grid item xs={3} key={i}>
              <div className={classes.companyItem}>
                <Link
                  key={i}
                  to={`${url}/${company.id}`}
                  className={`${classes.linkItem} ${classes.divPointer}`}
                >
                  <Typography
                    variant="h6"
                    gutterBottom
                    className={classes.typographyLink}
                  >
                    {"Company name: " + company.name}
                  </Typography>
                  <Typography
                    variant="body1"
                    gutterBottom
                    className={classes.typographyLink}
                  >
                    {"Company Id: " + company.id}
                  </Typography>
                  <Typography
                    variant="body2"
                    gutterBottom
                    className={classes.typographyLink}
                  >
                    {"Products updated: " + company.productCount}
                  </Typography>
                </Link>
              </div>
            </Grid>
          ))}
        </Grid>
        <Grid container direction="column" spacing={3}>
          <Switch>
            <Route path={`${path}/:companyId`}>
              <CategoriesSupPage
                categorySelectIds={categorySelectIds}
                nestedCategoryList={nestedCategoryList}
                setCategorySelectIds={setCategorySelectIds}
                setNestedCategoryList={setNestedCategoryList}
              />
            </Route>
          </Switch>
          <Switch>
            <Route path={`${path}/:companyId/:categoryId`}>
              <ProductSubPage />
            </Route>
          </Switch>
        </Grid>
      </div>
    </React.Fragment>
  ;
};

function SubToProducts() {
  const { data, loading } = useSubscription(GET_PRODUCT_INFO_SUB);
  if(!loading) console.table(data);
}
