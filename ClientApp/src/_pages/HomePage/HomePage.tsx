import { Grid, makeStyles } from "@material-ui/core";

import React from "react";

import { ParseDataSegment } from "../../_components";

const useStyles = makeStyles((theme) => ({
  rootBox: {
    marginTop: theme.spacing(5),
    marginBottom: theme.spacing(15),
  },
  urlField: {
    margin: theme.spacing(5),
  },
}));
export const HomePage: React.FC = () => {
  const classes = useStyles();

  // const handleGetShopsRequest = async () => {};

  return (
    <React.Fragment>
      <Grid
        container
        spacing={2}
        direction="row"
        justify="center"
        className={classes.rootBox}
      >
        <Grid container item xs={4}></Grid>
        <Grid item></Grid>
        <Grid item xs={4}></Grid>
      </Grid>
      <ParseDataSegment></ParseDataSegment>
    </React.Fragment>
  );
};
