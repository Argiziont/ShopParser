import { makeStyles, createStyles, Grid, Typography } from "@material-ui/core";
import React from "react";
import { NavLink } from "react-router-dom";


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
  const classes = useStyles()

  return (
    <Grid item xs={3} container justifyContent="center" direction="row">
      <Grid item>
        {MenuPages.map((pageName,i) => <NavLink
          key={i}
        to={`/${pageName}`}
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
      </NavLink>)}
      </Grid>
      
      </Grid>
  );
};
