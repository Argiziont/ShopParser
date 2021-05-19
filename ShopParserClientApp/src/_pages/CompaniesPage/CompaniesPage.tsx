import {
  CircularProgress,
  Grid,
  makeStyles,
  Typography,
} from "@material-ui/core";

import React, { useEffect, useState } from "react";
import { Link, Route, Switch, useRouteMatch } from "react-router-dom";
import { IResponseCompany, UserActions } from "../../_actions";
import { CategoriesSupPage } from "./CategoriesSupPage";

const useStyles = makeStyles(() => ({
  divPointer: {
    cursor: "pointer",
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
  linkItem: {
    textDecoration: "none",
  },
}));
export const CompaniesPage: React.FC = () => {
  const classes = useStyles();
  const { url, path } = useRouteMatch();
  
  const [companyList, setCompanyList] = useState<IResponseCompany[]>();
  const [isCompaniesLodaing, setIsCompanysLodaing] = useState<boolean>(false);

  useEffect(() => {
    let isMounted = true;
    setIsCompanysLodaing(true);

    UserActions.GetAllCompanys().then((companyList) => {
      if (isMounted) {
        setCompanyList(companyList);
        setIsCompanysLodaing(false);
      }
    });
    return () => {
      isMounted = false;
    }; // use effect cleanup to set flag false, if unmounted
  }, []);

  return isCompaniesLodaing ? (
    <CircularProgress color="inherit" />
  ) : (
    <React.Fragment>
      <div className={classes.gridListRoot}>
        <Grid container spacing={3}>
          {companyList?.map((company, i) => (
            <Grid item xs={3} key={i}>
              <div className={classes.companyItem}>
                <Link
                  key={i}
                  to={`${url}/:${company.id}`}
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
        <Grid container spacing={3}>
          <Switch>
            <Route exact path={path}></Route>
            <Route path={`${path}/:companyId`}>
              <CategoriesSupPage />
            </Route>
          </Switch>
        </Grid>
      </div>
    </React.Fragment>
  );
};
