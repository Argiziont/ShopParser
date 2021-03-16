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

import { ICarAdvert } from "../_actions";

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
  rootGrid: {
    marginBottom: theme.spacing(4),

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
    padding: "200% 0px 0px 0px",
  },
  carouselCard: {
    height: "100%",
    width: "100%",
  },
}));

export const ParseDataSegment: React.FC<ICarAdvert> = (
  CartAdvert: ICarAdvert
) => {
  const classes = useStyles();

  const listImgs = CartAdvert.imageUrls?.map((imageUrl, i) => (
    <Card key={i}>
      <CardHeader title={CartAdvert.title} subheader={CartAdvert.scuCode} />
      <CardMedia
        className={classes.media}
        image={imageUrl}
        title={CartAdvert.title}
      />
    </Card>
  ));

  return (
    <React.Fragment>
      <Grid container spacing={2} direction="row" justify="center" className={classes.rootGrid}>
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
            label="CompanyName"
            value={CartAdvert.companyName}
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
            label="PositivePercent"
            value={CartAdvert.positivePercent}
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
            label="RatingsPerLastYear"
            value={CartAdvert.ratingsPerLastYear}
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
            label="Title"
            value={CartAdvert.title}
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
            value={CartAdvert.scuCode}
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
            value={CartAdvert.presence}
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
            value={CartAdvert.description}
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
            value={CartAdvert.price}
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
            value={CartAdvert.fullPrice}
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
            value={CartAdvert.optPrice}
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
            value={CartAdvert.currency}
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
            value={CartAdvert.fullCurrency}
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
            value={CartAdvert.optCurrency}
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
            {listImgs}
          </Carousel>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};
