import {
  Grid,
  makeStyles,
  Typography,
  Link,
  Button,
  TextField,
} from "@material-ui/core";
import CloudUploadIcon from "@material-ui/icons/CloudUpload";
import React, { useEffect, useState } from "react";
import { IProductJson, IResponseProduct, IResponseShop } from "../_actions";

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

export const ParseDataSegment: React.FC = () => {
  const [productList, setProductList] = useState<IResponseProduct[]>();
  const [shopList, setShopList] = useState<IResponseShop[]>();
  const [isLodaing, setIsLodaing] = useState<boolean>(false);
  const [checkedProduct, setCheckedProduct] = useState<
    IProductJson | undefined
  >();

  const classes = useStyles();

  useEffect(() => {
    let isMounted = true;
    setIsLodaing(true);

    UserActions.GetAllShops().then((shopList) => {
      if (isMounted) {
        setShopList(shopList);
        setIsLodaing(false);
      }
    });
    return () => {
      isMounted = false;
    }; // use effect cleanup to set flag false, if unmounted
  }, []);

  const preventDefault = (event: React.SyntheticEvent) =>
    event.preventDefault();

  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };

  const shopsBlocks = isLodaing ? (
    <div></div>
  ) : (
    shopList?.map((shop) => {
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
    })
  );

  const productsBlocks = productList?.map((product) => {
    return (
      <Grid item xs key={product.id} zeroMinWidth>
        <div
          className={`${classes.shopItem} ${classes.divPointer}`}
          onClick={() => handleProductClick(product.id)}
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
    checkedProduct == undefined ? (
      <div></div>
    ) : (
      <Grid item xs zeroMinWidth>
        <div className={classes.shopItem}>
          <Typography variant="h5" gutterBottom>
            {checkedProduct.title}
          </Typography>
          <Typography variant="h6" gutterBottom>
            {checkedProduct.companyName}
          </Typography>
          <Typography variant="body2" gutterBottom>
            {checkedProduct.presence}
          </Typography>
          <Typography variant="body2" gutterBottom>
            {checkedProduct.scuCode}
          </Typography>
          <Typography variant="h6" gutterBottom>
            {checkedProduct.price + " " + checkedProduct.currency}
          </Typography>
          <Typography variant="body1" gutterBottom>
            {checkedProduct.description}
          </Typography>
          <Typography variant="h6" gutterBottom>
            {"ImageUrls"}
          </Typography>
          {checkedProduct.imageUrls?.map((imgUrl, i) => (
            <Link
              key={i}
              href={imgUrl}
              onClick={preventDefault}
              color="inherit"
            >
              <Typography variant="body2" gutterBottom>
                {imgUrl}
              </Typography>
            </Link>
          ))}
          <Typography variant="body2" gutterBottom>
            {"Id: " + checkedProduct.externalId}
          </Typography>
          <Typography variant="body2" gutterBottom>
            {"Sync date: " + checkedProduct.syncDate}
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
        }
      }
    } catch {}
  };

  const handleProductClick = async (id: number | undefined) => {
    if (id != undefined) {
      const response = await UserActions.GetProductById(id);

      if (response != undefined) {
        setCheckedProduct(response);
        scrollToTop();
      }
    }
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

          <Grid item key={-1}>
            <div className={classes.shopItem}>
              <TextField label="Shop URL" variant="standard" value={"Null"} />

              <Button
                variant="contained"
                endIcon={<CloudUploadIcon />}
                //onClick={handleGetShopsRequest}
              >
                {"Submit"}
              </Button>
            </div>
          </Grid>
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
