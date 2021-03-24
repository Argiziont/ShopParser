import { Grid, makeStyles, Typography } from "@material-ui/core";

import React, { useState } from "react";
import { IResponseProduct, IResponseShop } from "../_actions";

import { UserActions } from "../_actions";

//import 'fontsource-roboto';
//import Carousel from "react-material-ui-carousel";
//import { ICarAdvert } from "../_actions";

const useStyles = makeStyles((theme) => ({
  rootBox: {
    //marginBottom: theme.spacing(1),
    //margin: "0px 15px 0px 15px",
    background: "#D3D3D3",
    border: 0,
    borderRadius: 16,
    color: theme.palette.primary.main,
    padding: "0 30px",
  },
  rootGrid: {
    //marginBottom: theme.spacing(4),
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
    background: "#D3D3D3",
    border: 0,
    borderRadius: 16,
    padding: "15px 15px",
    minWidth: "250px",
  },
  divPointer: {
    cursor: "pointer",
  },
}));

interface ParseDataSegmentProps {
  ShopList: IResponseShop[] | undefined;
}
export const ParseDataSegment: React.FC<ParseDataSegmentProps> = (
  Shops: ParseDataSegmentProps
) => {
  const [productList, setProductList] = useState<IResponseProduct[]>();
  const [checedProduct, setChecedProduct] = useState<
    IResponseProduct | undefined
  >();

  const classes = useStyles();

  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };
  const shopsBlocks = Shops.ShopList?.map((shop) => {
    return (
      <Grid item key={shop.id}>
        <div
          className={`${classes.shopItem} ${classes.divPointer}`}
          onClick={() => handleGetProductRequest(shop.id)}
        >
          <Typography variant="h6" gutterBottom>
            {shop.name}
          </Typography>
          <Typography variant="body1" gutterBottom>
            {"Shop Id: " + shop.externalId}
          </Typography>
        </div>
      </Grid>
    );
  });
  const productsBlocks = productList?.map((product) => {
    return (
      <Grid item xs key={product.id} zeroMinWidth>
        <div
          className={`${classes.shopItem} ${classes.divPointer}`}
          onClick={() => handleProductClick(product)}
        >
          <Typography variant="h6" gutterBottom noWrap>
            {product.title}
          </Typography>
          <Typography variant="body1" gutterBottom>
            {"Price: " + product.price}
          </Typography>
        </div>
      </Grid>
    );
  });

  const productBlocks =
    checedProduct == undefined ? (
      <div></div>
    ) : (
      <Grid item xs zeroMinWidth>
        <div className={classes.shopItem}>
          <Typography variant="h5" gutterBottom>
            {checedProduct.title}
          </Typography>
          <Typography variant="h6" gutterBottom>
            {"Price: " + checedProduct.price}
          </Typography>
          <Typography variant="body1" gutterBottom>
            {checedProduct.description}
          </Typography>
          <Typography variant="body2" gutterBottom>
            {"Id: " + checedProduct.externalId}
          </Typography>
          <Typography variant="body2" gutterBottom>
            {"Sync date: " + checedProduct.syncDate}
          </Typography>
        </div>
      </Grid>
    );

  const handleGetProductRequest = async (id: number | undefined) => {
    try {
      if (id != undefined) {
        const response = await UserActions.GetAllProductInShop(id);

        if (response != undefined) {
          setProductList(response);
          console.log(response);
        }
      }
    } catch {}
  };
  const handleProductClick = (product: IResponseProduct) => {
    setChecedProduct(product);
    scrollToTop();
  };
  return (
    <React.Fragment>
      <Grid
        container
        spacing={3}
        direction="row"
        justify="center"
        className={classes.rootGrid}
      >
        <Grid
          container
          item
          xs={3}
          spacing={3}
          justify="flex-start"
          direction="column"
          alignItems="flex-start"
        >
          {shopsBlocks}
        </Grid>
        <Grid
          container
          item
          xs={3}
          spacing={3}
          justify="flex-start"
          direction="column"
          alignItems="flex-start"
        >
          {productsBlocks}
        </Grid>
        <Grid
          container
          item
          xs={3}
          spacing={3}
          justify="flex-start"
          direction="column"
          alignItems="flex-end"
        >
          {productBlocks}
        </Grid>
      </Grid>
    </React.Fragment>
  );
};
