import {
  Grid,
  GridList,
  GridListTile,
  GridListTileBar,
  IconButton,
  makeStyles,
  Typography,
} from "@material-ui/core";

import React from "react";
import {
  BrowserRouter as Router,
  Route,
  Switch,
  useRouteMatch,
} from "react-router-dom";
import { CompaniesPage } from "../CompaniesPage";
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
            <Grid item  container spacing={3}>
              <Switch>
                <Route exact path={path}>
                  
                </Route>
                <Route path={`${path}/Company`} component={() =><CompaniesPage/>}>
                
                </Route>
                <Route path={`${path}/Categories`}>
                  <Typography variant="h6" gutterBottom noWrap>
                    {"Categories"}
                  </Typography>
                </Route>
                <Route path={`${path}/Products`}>
                  <Typography variant="h6" gutterBottom noWrap>
                    {"Products"}
                  </Typography>
                </Route>
              </Switch>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

