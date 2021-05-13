import {
  Grid,
  makeStyles,
  Typography,
} from "@material-ui/core";

import React from "react";
import {
  Route,
  Switch,
  useRouteMatch,
} from "react-router-dom";

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
       
  
      </Grid>

    </React.Fragment>
  );
};
