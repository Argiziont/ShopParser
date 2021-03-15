import {
  Card,
  CardMedia,
  Grid,
  makeStyles,
  CardHeader,
  TextField,
} from "@material-ui/core";

import React from "react";
import Carousel from "react-material-ui-carousel";

const useStyles = makeStyles((theme) => ({
  rootBox: {
    //marginBottom: theme.spacing(1),
    margin: "0px 15px 0px 15px",
    background: "#D3D3D3",
    border: 0,
    borderRadius: 16,
    color: theme.palette.primary.main,
    padding: "0 30px",
  },
  dataFields: {
    marginBottom: theme.spacing(2),
  },
  dataMultiline: {
    marginBottom: theme.spacing(2),
  },
  dataImage: {
    maxWidthth: "100px",
    maxHeightht: "100px",
  },
  media: {
    height: "100%",
    paddingTop: "100%", // 16:9
  },
}));

export const ParseDataSegment: React.FC = () => {
  const classes = useStyles();
  const images = [
    {
      url:
        "https://i.ytimg.com/an_webp/xGeorhZ3-yQ/mqdefault_6s.webp?du=3000&sqp=COCOvYIG&rs=AOn4CLAewUqSqnIc9M2jsOiAK4TNP7y43A",
    },
    {
      url:
        "https://i.ytimg.com/an_webp/xGeorhZ3-yQ/mqdefault_6s.webp?du=3000&sqp=COCOvYIG&rs=AOn4CLAewUqSqnIc9M2jsOiAK4TNP7y43A",
    },
    {
      url:
        "https://i.ytimg.com/an_webp/xGeorhZ3-yQ/mqdefault_6s.webp?du=3000&sqp=COCOvYIG&rs=AOn4CLAewUqSqnIc9M2jsOiAK4TNP7y43A",
    },
    {
      url:
        "https://i.ytimg.com/an_webp/xGeorhZ3-yQ/mqdefault_6s.webp?du=3000&sqp=COCOvYIG&rs=AOn4CLAewUqSqnIc9M2jsOiAK4TNP7y43A",
    },
  ];
  return (
    <React.Fragment>
      <Grid container spacing={2} direction="row" justify="center">
        <Grid
          item
          container
          xs={3}
          justify="center"
          direction="column"
          alignItems="center"
          className={classes.rootBox}
        >
          <TextField
            className={classes.dataFields}
            fullWidth
            inputProps={{
              readOnly: true,
              disabled: true,
            }}
            label="Title"
            value={"Something"}
            color="secondary"
            variant="standard"
          />
          <TextField
            className={classes.dataFields}
            fullWidth
            inputProps={{
              readOnly: true,
              disabled: true,
            }}
            label="ScuCode"
            value={"Something"}
            color="secondary"
            variant="standard"
          />
          <TextField
            className={classes.dataFields}
            fullWidth
            inputProps={{
              readOnly: true,
              disabled: true,
            }}
            label="Presence"
            value={"Something"}
            color="secondary"
            variant="standard"
          />
          <TextField
            className={classes.dataMultiline}
            fullWidth
            inputProps={{
              readOnly: true,
              disabled: true,
            }}
            label="Description"
            multiline
            rows={4}
            value={
              "Something\nSomething\nSomething\nSomething\nSomething\nSomething\n"
            }
            color="secondary"
            variant="standard"
          />
          <TextField
            className={classes.dataFields}
            fullWidth
            inputProps={{
              readOnly: true,
              disabled: true,
            }}
            label="Price"
            value={"Something"}
            color="secondary"
            variant="standard"
          />
          <TextField
            className={classes.dataFields}
            fullWidth
            inputProps={{
              readOnly: true,
              disabled: true,
            }}
            label="FullPrice"
            value={"Something"}
            color="secondary"
            variant="standard"
          />
          <TextField
            className={classes.dataFields}
            fullWidth
            inputProps={{
              readOnly: true,
              disabled: true,
            }}
            label="OptPrice"
            value={"Something"}
            color="secondary"
            variant="standard"
          />
          <TextField
            className={classes.dataFields}
            fullWidth
            inputProps={{
              readOnly: true,
              disabled: true,
            }}
            label="Currency"
            value={"Something"}
            color="secondary"
            variant="standard"
          />
          <TextField
            className={classes.dataFields}
            fullWidth
            inputProps={{
              readOnly: true,
              disabled: true,
            }}
            label="FullCurrency"
            value={"Something"}
            color="secondary"
            variant="standard"
          />
          <TextField
            className={classes.dataFields}
            fullWidth
            inputProps={{
              readOnly: true,
              disabled: true,
            }}
            label="OptCurrency"
            value={"Something"}
            color="secondary"
            variant="standard"
          />
        </Grid>
        <Grid
          item
          container
          xs={3}
          justify="center"
          direction="column"
          alignItems="center"
          className={classes.rootBox}
        >
          <Carousel
            navButtonsAlwaysInvisible={false}
            navButtonsAlwaysVisible={false}
          >
            <Card>
              <CardHeader
                title="Shrimp and Chorizo Paella"
                subheader="September 14, 2016"
              />
              <CardMedia
                className={classes.media}
                image="http://placehold.it/350x150"
                title="Paella dish"
              />
            </Card>
            <Card>
              <CardHeader
                title="Shrimp and Chorizo Paella"
                subheader="September 14, 2016"
              />
              <CardMedia
                className={classes.media}
                image="http://placehold.it/255x150"
                title="Paella dish"
              />
            </Card>
            <Card>
              <CardHeader
                title="Shrimp and Chorizo Paella"
                subheader="September 14, 2016"
              />
              <CardMedia
                className={classes.media}
                image="http://placehold.it/310x150"
                title="Paella dish"
              />
            </Card>
          </Carousel>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};
