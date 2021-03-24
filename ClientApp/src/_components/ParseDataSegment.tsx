import {
  Card,
  CardMedia,
  Grid,
  makeStyles,
  CardHeader,
  TextField,
  Typography,
} from "@material-ui/core";

import React from "react";
import Carousel from "react-material-ui-carousel";
import { IResponseShop } from "../_actions";

//import 'fontsource-roboto';

//import { ICarAdvert } from "../_actions";

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
  shopItem: {
    justifyContent: 'center',
    alignItems: 'center',
    margin: "0px 0px 15px 15px",
    background: "#D3D3D3",
    border: 0,
    borderRadius: 16,
    padding: "15px 15px",
    cursor: "pointer",
    minWidth: "250px"
  },
  shodListContainer: {
    padding:"15px 0px 15px 0px",
  },
}));

interface ParseDataSegmentProps {
  ShopList: IResponseShop[] | undefined;
}
export const ParseDataSegment: React.FC<ParseDataSegmentProps> = (
  Shops: ParseDataSegmentProps
) => {
  const classes = useStyles();

  // const listImgs = CartAdvert.imageUrls?.map((imageUrl, i) => (
  //   <Card key={i}>
  //     <CardHeader title={CartAdvert.title} subheader={CartAdvert.scuCode} />
  //     <CardMedia
  //       className={classes.media}
  //       image={imageUrl}
  //       title={CartAdvert.title}
  //     />
  //   </Card>
  // ));
  const shopsBlocks = Shops.ShopList?.map((shop) => {
    return<div key={shop.id} className={classes.shopItem} onClick={() => console.log(shop.externalId)}>
      <Typography variant="h6" gutterBottom >
        {shop.name}
      </Typography>
    </div>
  });
  return (
    <React.Fragment>
      <Grid
        container
        spacing={2}
        direction="row"
        justify="center"
        className={classes.rootGrid}
      >
        <Grid
          item
          container
          xs={3}
          justify="center"
          direction="column"
          alignItems="center"
          className={classes.shodListContainer}
        >
          {shopsBlocks}
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
          <TextField
            className={classes.dataFields}
            fullWidth
            inputProps={{
              readOnly: true,
              disabled: true,
            }}
            label="CompanyName"
            //value={CartAdvert.companyName}
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
            //value={CartAdvert.description}
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
            {/* {listImgs} */}
          </Carousel>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};
