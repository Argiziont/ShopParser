import { Button, Grid, makeStyles, TextField } from "@material-ui/core";
import CloudUploadIcon from "@material-ui/icons/CloudUpload";
import React from "react";

const useStyles = makeStyles((theme) => ({
  rootBox: {
    marginTop: theme.spacing(5),
  },
  urlField: {
    margin: theme.spacing(5),
  },
}));
export const HomePage: React.FC = () => {
  const classes = useStyles();
  return (
    <React.Fragment>
      <Grid
        container
        spacing={2}
        direction="row"
        justify="center"
        className={classes.rootBox}
      >
        <Grid container xs={4}></Grid>

        <Grid
          container
          xs={4}
          spacing={2}
          justify="center"
          alignItems="flex-end"
        >
          <Grid item>
            <TextField label="Site URL" variant="standard" />
          </Grid>
          <Grid item>
            <Button variant="contained" endIcon={<CloudUploadIcon />}>
              {"Submit"}
            </Button>
          </Grid>
        </Grid>
        <Grid container xs={4}></Grid>
      </Grid>
    </React.Fragment>
  );
};
