import { makeStyles, createStyles, Grid, Typography } from "@material-ui/core";
import React from "react";
import { Link, useRouteMatch } from "react-router-dom";


export const MenuPages = ["Company", "Categories", "Products"];

export const HomePageRouting: React.FC = () => {
  const useStyles = makeStyles((theme) =>
    createStyles({
      linkItem: {
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
    })
  );
  const classes = useStyles();
  const { url } = useRouteMatch();


  return (
    <Grid item xs={3} container justify="center" direction="row">
      <Grid item>
        {MenuPages.map((pageName,i) => <Link
          key={i}
        to={`${url}/${pageName}`}
        className={`${classes.linkItem} ${classes.divPointer}`}
      >
        <Typography
          variant="h6"
          gutterBottom
          noWrap
          className={classes.typographyLink}
        >
          {pageName}
        </Typography>
      </Link>)}
      </Grid>
      
      </Grid>
  );
};
