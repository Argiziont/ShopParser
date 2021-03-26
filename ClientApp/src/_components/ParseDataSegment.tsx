import {
  Grid,
  makeStyles,
  Typography,
  Link,
  Button,
  TextField,
  CircularProgress,
  TablePagination,
  Snackbar,
} from "@material-ui/core";
import CloudUploadIcon from "@material-ui/icons/CloudUpload";
import React, { useEffect, useState } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";

import {
  IProductJson,
  IResponseProduct,
  IResponseShop,
  UserActions,
} from "../_actions";
import { ApiUrl, SnackbarMessage } from "../_services";
import MuiAlert, { AlertProps, Color } from "@material-ui/lab/Alert";

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
  shopOuterItem: {
    border: 0,
    borderRadius: 16,
    minWidth: "250px",
    maxWidth: "350px",
  },
  divPointer: {
    cursor: "pointer",
  },
}));

function Alert(props: AlertProps) {
  return <MuiAlert elevation={6} variant="filled" {...props} />;
}

export const ParseDataSegment: React.FC = () => {
  const [productList, setProductList] = useState<IResponseProduct[]>();
  const [shopList, setShopList] = useState<IResponseShop[]>();
  const [currentShopId, setCurrentShopId] = useState<number>();
  const [isDivHover, setIsDivHover] = useState<boolean>();
  const [numberOfProductsInShop, setNumberOfProductsInShop] = useState<number>(
    0
  );
  const [isShopsLodaing, setIsShopsLodaing] = useState<boolean>(false);
  const [isShopDivExtended, setIsShopDivExtended] = useState<number>(-1);
  const [isProductDivExtended, setIsProductDivExtended] = useState<number>(-1);
  const [isProductsLodaing, setIsProductsLodaing] = useState<boolean>(false);
  const [shopUrl, setShopUrl] = useState<string>("");
  const [checkedProduct, setCheckedProduct] = useState<
    IProductJson | undefined
  >();
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(10);
  const [openSnack, setOpenSnack] = React.useState<boolean>(false);
  const [snackPack, setSnackPack] = React.useState<SnackbarMessage[]>([]);
  const [messageInfo, setMessageInfo] = React.useState<
    SnackbarMessage | undefined
  >(undefined);

  const classes = useStyles();

  useEffect(() => {
    let isMounted = true;
    setIsShopsLodaing(true);

    const connection = new HubConnectionBuilder()
      .withUrl(ApiUrl + "/hubs/DataFetchHub")
      .withAutomaticReconnect()
      .build();

    connection
      .start()
      .then(() => {
        connection.on("ReceiveMessage", (message) => {
          handleSnackOpen(message, "info")();
        });
      })

      .catch((e) => console.log("Connection failed: ", e));
    UserActions.GetAllShops().then((shopList) => {
      if (isMounted) {
        setShopList(shopList);
        setIsShopsLodaing(false);
      }
    });
    return () => {
      isMounted = false;
    }; // use effect cleanup to set flag false, if unmounted
  }, []);
  useEffect(() => {
    if (snackPack.length && !messageInfo) {
      // Set a new snack when we don't have an active one
      setMessageInfo({ ...snackPack[0] });
      setSnackPack((prev) => prev.slice(1));
      setOpenSnack(true);
    } else if (snackPack.length && messageInfo && open) {
      // Close an active snack when a new one is added
      setOpenSnack(false);
    }
  }, [snackPack, messageInfo, openSnack]);

  const preventDefault = (event: React.SyntheticEvent) => {
    return event.preventDefault();
  };
  const handleSetPage = async (pageNumber: number, rowsCount = rowsPerPage) => {
    setPage(pageNumber);
    handleGetProductRequest(currentShopId, pageNumber, rowsCount);
  };
  const handleChangePage = (
    event: React.MouseEvent<HTMLButtonElement> | null,
    newPage: number
  ) => {
    handleSetPage(newPage);
  };
  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const rowsPerPageParsed: number = parseInt(event.target.value, 10);
    setRowsPerPage(rowsPerPageParsed);
    handleSetPage(0, rowsPerPageParsed);
  };
  const handleShopUrlChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setShopUrl(event.target.value);
  };
  const handleShopUrlUploadClick = async () => {
    try {
      if (shopUrl != undefined) {
        const response = await UserActions.AddShopByUrl(shopUrl);

        if (response != undefined) {
          UserActions.GetAllShops().then((shopList) => {
            setShopList(shopList);
          });
        }
      }
    } catch {}
  };
  const handleGetProductRequest = async (
    id: number | undefined,
    page: number | undefined,
    rowsCount: number | undefined
  ) => {
    try {
      if (id != undefined && page != undefined && rowsCount != undefined) {
        setCurrentShopId(id);
        setIsProductsLodaing(true);
        const response = await UserActions.GetProductByIdAndPage(
          id,
          page,
          rowsCount
        );
        setIsProductsLodaing(false);

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
  const handleSnackClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === "clickaway") {
      return;
    }

    setOpenSnack(false);
  };
  const handleSnackExited = () => {
    setMessageInfo(undefined);
  };
  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };
  const handleSnackOpen = (message: string, type: Color) => () => {
    setSnackPack((prev) => [
      ...prev,
      { message, key: new Date().getTime(), type },
    ]);
  };

  const snackBarContainter = (
    <Snackbar
      key={messageInfo ? messageInfo.key : undefined}
      anchorOrigin={{
        vertical: "top",
        horizontal: "right",
      }}
      open={openSnack}
      autoHideDuration={4000}
      onClose={handleSnackClose}
      onExited={handleSnackExited}
    >
      <Alert
        onClose={handleSnackClose}
        severity={messageInfo ? messageInfo.type : undefined}
      >
        {messageInfo ? messageInfo.message : undefined}
      </Alert>
    </Snackbar>
  );
  const shopsBlocks = isShopsLodaing ? (
    <CircularProgress color="inherit" />
  ) : (
    shopList?.map((shop, i) => {
      return (
        <Grid item key={shop.id}>
          <div
            className={`${classes.shopOuterItem} ${classes.divPointer}`}
            onMouseEnter={() => setIsShopDivExtended(i)}
            onMouseLeave={() => setIsShopDivExtended(-1)}
            onClick={() => {
              if (!isDivHover) {
                console.log("Click");
              }
            }}
            style={
              isShopDivExtended == i
                ? {
                    padding: "0px 15px 0px 0px",
                    background: "#be0000",
                    transition: "padding 0.15s ease-in, background 0s",
                  }
                : {
                    padding: "0px 0px 0px 0px",
                    background: "#ffff",
                    transition: "padding 0.2s ease-in, background 1s",
                  }
            }
          >
            <div
              className={`${classes.shopItem} ${classes.divPointer}`}
              onMouseEnter={() => setIsDivHover(true)}
              onMouseLeave={() => setIsDivHover(false)}
              onClick={() => {
                handleGetProductRequest(shop.id, page, rowsPerPage);
                setNumberOfProductsInShop(
                  shop.productCount != undefined ? shop.productCount : 0
                );
              }}
            >
              <Typography variant="h6" gutterBottom>
                {shop.name}
              </Typography>
              <Typography variant="body1" gutterBottom>
                {"Shop Id: " + shop.externalId}
              </Typography>
              <Typography variant="body2" gutterBottom>
                {"Products updated: " + shop.productCount}
              </Typography>
            </div>
          </div>
        </Grid>
      );
    })
  );
  const productBlockPagination =
    isProductsLodaing || productList == undefined || productList.length == 0 ? (
      <div></div>
    ) : (
      <Grid item xs>
        <div className={classes.shopItem} style={{ width: "95%" }}>
          <TablePagination
            component="div"
            count={numberOfProductsInShop}
            page={page}
            onChangePage={handleChangePage}
            rowsPerPage={rowsPerPage}
            rowsPerPageOptions={[10, 25, 50, 75, 100]}
            onChangeRowsPerPage={handleChangeRowsPerPage}
          />
        </div>
      </Grid>
    );
  const productsBlocks = isProductsLodaing ? (
    <CircularProgress color="inherit" />
  ) : (
    productList?.map((product, i) => {
      return (
        <Grid item xs key={product.id} zeroMinWidth>
          <div
            className={`${classes.shopOuterItem} ${classes.divPointer}`}
            onMouseEnter={() => setIsProductDivExtended(i)}
            onMouseLeave={() => setIsProductDivExtended(-1)}
            onClick={() => {
              if (!isDivHover) {
                console.log("Click");
              }
            }}
            style={
              isProductDivExtended == i
                ? {
                    padding: "0px 15px 0px 0px",
                    background: "#be0000",
                    transition: "padding 0.15s ease-in, background 0s",
                  }
                : {
                    padding: "0px 0px 0px 0px",
                    background: "#ffff",
                    transition: "padding 0.2s ease-in, background 1s",
                  }
            }
          >
            <div
              className={`${classes.shopItem} ${classes.divPointer}`}
              onClick={() => handleProductClick(product.id)}
              onMouseEnter={() => setIsDivHover(true)}
              onMouseLeave={() => setIsDivHover(false)}
            >
              <Typography variant="h6" gutterBottom noWrap>
                {product.title}
              </Typography>
              <Typography variant="body1" gutterBottom>
                {"Price: " + product.price}
              </Typography>
            </div>
          </div>
        </Grid>
      );
    })
  );

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
          {checkedProduct.imageUrls?.length == 0 ? (
            <div></div>
          ) : (
            <Typography variant="h6" gutterBottom>
              {"ImageUrls"}
            </Typography>
          )}

          {checkedProduct.imageUrls?.map((imgUrl, i) => (
            <Link
              key={i}
              href={imgUrl}
              onClick={preventDefault}
              color="inherit"
            >
              <Typography variant="body2" gutterBottom noWrap>
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
          <Grid item key={-1}>
            <div className={classes.shopItem}>
              <TextField
                label="Shop URL"
                variant="standard"
                value={shopUrl}
                onChange={handleShopUrlChange}
              />

              <Button
                variant="contained"
                endIcon={<CloudUploadIcon />}
                onClick={handleShopUrlUploadClick}
              >
                {"Submit"}
              </Button>
            </div>
          </Grid>
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
          {productBlockPagination}
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
          {snackBarContainter}
        </Grid>
      </Grid>
    </React.Fragment>
  );
};
