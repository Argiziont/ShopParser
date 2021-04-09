//Important react and microsoft import
import React, { useEffect, useState } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";

//Material UI imports
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
  IconButton,
} from "@material-ui/core";
import CloudUploadIcon from "@material-ui/icons/CloudUpload";
import SortIcon from "@material-ui/icons/Sort";
import CloseIcon from "@material-ui/icons/Close";
import MuiAlert, { Color } from "@material-ui/lab/Alert";

//Self project imports
import {
  IProductJson,
  IResponseNestedCategory,
  IResponseProduct,
  IResponseShop,
  UserActions,
} from "../_actions";
import { ApiUrl, SnackbarMessage } from "../_services";
import {
  SelectSortTypeDialog,
  SortTypes,
  NestedCategoryList,
} from "../_components";

const useStyles = makeStyles((theme) => ({
  rootBox: {
    background: "#D3D3D3",
    border: 0,
    borderRadius: 16,
    color: theme.palette.primary.main,
    padding: "0 30px",
  },
  shopItem: {
    maxWidth: "320px",
    minWidth: "300px",
    background: "#D3D3D3",
    border: 0,
    borderRadius: 16,
    padding: "15px 15px",
  },
  productItem: {
    maxWidth: "400px",
    minWidth: "350px",
    background: "#D3D3D3",
    border: 0,
    borderRadius: 16,
    padding: "15px 15px",
  },
  shopInput: {
    maxWidth: "150px",
  },
  shopOuterItem: {
    border: 0,
    borderRadius: 16,
    //minWidth: "250px",
    //maxWidth: "350px",
  },
  divPointer: {
    cursor: "pointer",
  },
  divDefault: {
    cursor: "default",
  },
}));
export const ParseDataSegment: React.FC = () => {
  //Procucts states
  const [productList, setProductList] = useState<IResponseProduct[]>();
  const [isProductsLodaing, setIsProductsLodaing] = useState<boolean>(false);
  const [numberOfProductsInShop, setNumberOfProductsInShop] = useState<number>(
    0
  );
  const [checkedProduct, setCheckedProduct] = useState<
    IProductJson | undefined
  >();

  //Shop states
  const [shopList, setShopList] = useState<IResponseShop[]>();
  const [currentShopId, setCurrentShopId] = useState<number>();
  const [isShopsLodaing, setIsShopsLodaing] = useState<boolean>(false);
  const [isShopDivExtended, setIsShopDivExtended] = useState<number>(-1);
  const [shopUrl, setShopUrl] = useState<string>("");

  //Products Pagination states
  const [productPage, setProductPage] = React.useState(0);
  const [rowsPerProductPage, setRowsPerProductPage] = React.useState(10);

  //Snack states
  const [openSnack, setOpenSnack] = React.useState<boolean>(false);
  const [snackPack, setSnackPack] = React.useState<SnackbarMessage[]>([]);
  const [messageInfo, setMessageInfo] = React.useState<
    SnackbarMessage | undefined
  >(undefined);

  //SortBy dialog states
  const [openedSortBy, setOpenedSortBy] = React.useState(false);
  const [selectedSortByValue, setSelectedSortByValue] = React.useState(
    SortTypes[0]
  );

  //Categories states
  const [categoriesList, setCategoriesList] = useState<
    IResponseNestedCategory[]
  >();

  const classes = useStyles();

  //SignalR and page loading effect
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
      UserActions.GetSubCategories().then((categoryList) => {
        if (isMounted) {
          setShopList(shopList);
          if (categoryList != undefined) {
            const nestedArray: IResponseNestedCategory[] = new Array(1);
            nestedArray[0] = categoryList;
            setCategoriesList(nestedArray);
          }
          setIsShopsLodaing(false);
        }
      });
    });
    return () => {
      isMounted = false;
    }; // use effect cleanup to set flag false, if unmounted
  }, []);

  //SnackBar effect with snack dependency array
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

  //Product actions (product click/products page change/etc)
  const handleSetProductPage = async (
    pageNumber: number,
    rowsCount = rowsPerProductPage
  ) => {
    setProductPage(pageNumber);
    setCheckedProduct(undefined);
    handleGetProductRequestByShops(currentShopId, pageNumber, rowsCount);
  };
  const handleChangeProductPage = (
    event: React.MouseEvent<HTMLButtonElement> | null,
    newPage: number
  ) => {
    handleSetProductPage(newPage);
  };
  const handleChangeProductRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const rowsPerPageParsed: number = parseInt(event.target.value, 10);
    setRowsPerProductPage(rowsPerPageParsed);
    handleSetProductPage(0, rowsPerPageParsed);
  };
  const handleGetProductRequestByShops = async (
    id: number | undefined,
    page: number | undefined,
    rowsCount: number | undefined
  ) => {
    try {
      if (id != undefined && page != undefined && rowsCount != undefined) {
        setCurrentShopId(id);
        setCheckedProduct(undefined);
        setIsProductsLodaing(true);
        const response = await UserActions.GetProductByShopIdAndPage(
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

  //Shop Actions
  const handleShopShowProductsClick = (
    id: number | undefined,
    productCount: number | undefined
  ) => {
    if (id != undefined && productCount != undefined) {
      handleGetProductRequestByShops(id, productPage, rowsPerProductPage);
      setNumberOfProductsInShop(productCount != undefined ? productCount : 0);
    }
  };

  const handleShopsUpdate = () => {
    setIsShopsLodaing(true);
    UserActions.GetAllShops().then((shopList) => {
      setShopList(shopList);
      setIsShopsLodaing(false);
      setCheckedProduct(undefined);
      setProductList(undefined);
    });
  };
  const handleShopUrlChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setShopUrl(event.target.value);
  };
  const handleShopUrlUploadClick = async () => {
    try {
      if (shopUrl != undefined) {
        const response = await UserActions.AddShopByUrl(shopUrl);

        if (response != undefined) {
          handleShopsUpdate();
        }
      }
    } catch {}
  };

  //Actions for Snackbar proper work
  const handleSnackClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === "clickaway") {
      return;
    }

    setOpenSnack(false);
  };
  const handleSnackExited = () => {
    setMessageInfo(undefined);
  };
  const handleSnackOpen = (message: string, type: Color) => () => {
    setSnackPack((prev) => [
      ...prev,
      { message, key: new Date().getTime(), type },
    ]);
  };

  //Actions for Product component
  const handleExternalUrlClick = (url: string) => {
    window.location.assign(url);
  };

  //Actions for SortBy
  const handleOpenSortBy = () => {
    setOpenedSortBy(true);
  };

  const handleCloseSortBy = (value: string) => {
    setOpenedSortBy(false);
    setSelectedSortByValue(value);
  };

  //Smooth scroll to the top of page on product click
  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };

  //Material UI snackbar notifications component
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
      <MuiAlert
        action={
          <React.Fragment>
            <Button
              size="small"
              aria-label="close"
              color="inherit"
              onClick={handleShopsUpdate}
            >
              Update
            </Button>
            <IconButton
              size="small"
              aria-label="close"
              color="inherit"
              onClick={handleSnackClose}
            >
              <CloseIcon fontSize="small" />
            </IconButton>
          </React.Fragment>
        }
        elevation={6}
        variant="filled"
        severity={messageInfo ? messageInfo.type : undefined}
      >
        {messageInfo ? messageInfo.message : undefined}
      </MuiAlert>
    </Snackbar>
  );

  //Shop list component
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
            style={
              isShopDivExtended == i
                ? {
                    borderRadius: 18,
                    padding: "0px 10px 0px 0px",
                    background: "#be0000",
                    transition: "padding 0.15s ease-in, background 0s",
                  }
                : {
                    borderRadius: 18,
                    padding: "0px 0px 0px 0px",
                    background: "#ffff",
                    transition: "padding 0.2s ease-in, background 1s",
                  }
            }
          >
            <div
              className={`${classes.shopItem} ${classes.divPointer}`}
              onClick={() =>
                handleShopShowProductsClick(shop.id, shop.productCount)
              }
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

  //Product list component pagintaion
  const productBlockPagination =
    isProductsLodaing || productList == undefined || productList.length == 0 ? (
      <div></div>
    ) : (
      <Grid item>
        <div className={classes.shopItem} style={{ width: "100%" }}>
          <TablePagination
            component="div"
            count={numberOfProductsInShop}
            page={productPage}
            onChangePage={handleChangeProductPage}
            rowsPerPage={rowsPerProductPage}
            rowsPerPageOptions={[10, 25, 50, 75, 100]}
            onChangeRowsPerPage={handleChangeProductRowsPerPage}
          />
        </div>
      </Grid>
    );

  //Product list component
  const productsBlocks = isProductsLodaing ? (
    <CircularProgress color="inherit" />
  ) : (
    productList?.map((product) => {
      return (
        <Grid item key={product.id}>
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
    })
  );

  //Product which was chosen
  const productBlocks =
    checkedProduct == undefined ? (
      <div></div>
    ) : (
      <Grid item>
        <div className={classes.productItem}>
          <Typography variant="h5" gutterBottom>
            {checkedProduct.title}
          </Typography>
          <Typography variant="h6" gutterBottom>
            {checkedProduct.companyName}
          </Typography>
          <Typography variant="body1" gutterBottom>
            {checkedProduct.stringCategory}
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
          <>
            {checkedProduct.imageUrls?.map((imgUrl, i) => (
              <Link
                key={i}
                href={imgUrl}
                rel="noreferrer"
                onClick={() => handleExternalUrlClick(imgUrl)}
                color="inherit"
              >
                <Typography variant="body2" gutterBottom noWrap>
                  {imgUrl}
                </Typography>
              </Link>
            ))}
          </>
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
        spacing={10}
        direction="row"
        justify="center"
        style={{
          margin: 0,
          width: "100%",
        }}
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
          <Grid item>
            <div className={classes.shopItem}>
              <Grid
                item
                container
                spacing={3}
                direction="row"
                justify="center"
                alignItems="center"
              >
                <Grid item>
                  <TextField
                    variant="outlined"
                    disabled
                    value={selectedSortByValue}
                    color="secondary"
                    size="small"
                    className={classes.shopInput}
                    onChange={handleShopUrlChange}
                  />
                </Grid>
                <Grid item>
                  <Button
                    variant="contained"
                    endIcon={<SortIcon />}
                    onClick={handleOpenSortBy}
                  >
                    {"Sort by"}
                  </Button>
                  <SelectSortTypeDialog
                    selectedValue={selectedSortByValue}
                    open={openedSortBy}
                    onClose={handleCloseSortBy}
                  />
                </Grid>
              </Grid>
            </div>
          </Grid>
          {selectedSortByValue == "Shops" ? (
            <>
              <Grid item>
                <div className={classes.shopItem}>
                  <Grid
                    item
                    container
                    spacing={3}
                    direction="row"
                    justify="center"
                    alignItems="center"
                  >
                    <Grid item>
                      <TextField
                        label="Shop URL"
                        variant="outlined"
                        value={shopUrl}
                        size="small"
                        className={classes.shopInput}
                        onChange={handleShopUrlChange}
                      />
                    </Grid>
                    <Grid item>
                      <Button
                        variant="contained"
                        endIcon={<CloudUploadIcon />}
                        onClick={handleShopUrlUploadClick}
                      >
                        {"Submit"}
                      </Button>
                    </Grid>
                  </Grid>
                </div>
              </Grid>
              {shopsBlocks}
            </>
          ) : (
            <>
              {categoriesList == undefined ? (
                <></>
              ) : (
                <Grid item>
                  <div className={classes.shopItem}>
                    <NestedCategoryList
                      padding={0}
                      list={categoriesList}
                    ></NestedCategoryList>
                  </div>
                </Grid>
              )}
            </>
          )}
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
          <Grid
            container
            item
            spacing={3}
            justify="flex-start"
            direction="column"
            alignItems="flex-start"
          >
            <Grid item>{productBlockPagination}</Grid>
            <Grid item container spacing={3}>
              {productsBlocks}
            </Grid>
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
          {productBlocks}
        </Grid>
      </Grid>
      {snackBarContainter}
    </React.Fragment>
  );
};
