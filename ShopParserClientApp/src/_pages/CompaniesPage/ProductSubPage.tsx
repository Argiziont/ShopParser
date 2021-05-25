import { Grid, makeStyles, Typography } from "@material-ui/core";
import { Pagination, PaginationItem } from "@material-ui/lab";
import React, { useEffect, useState } from "react";
import { Link, useLocation, useParams, useRouteMatch } from "react-router-dom";
import { IResponseProduct, UserActions } from "../../_actions";

export const ProductSubPage: React.FC = () => {
  const useStyles = makeStyles((theme) => ({
    outlinedItem: {
      maxWidth: "320px",
      minWidth: "300px",
      background: "#D3D3D3",
      border: 0,
      borderRadius: 16,
      padding: "15px 15px",
    },
    divPointer: {
      cursor: "pointer",
    },
  }));

  const classes = useStyles();

  const [productsList, setProductsList] = useState<IResponseProduct[]>();
  const [numberOfProductsInTotal, setNumberOfProductsInTotal] =
    useState<number>();
  const [productsListIsLoading, setProductsListIsLoading] = useState<boolean>();

  const { companyId, categoryId } =
    useParams<Record<string, string | undefined>>();

  const search = useLocation().search;
  const query = new URLSearchParams(search);
  const productPage = parseInt(query.get("page") || "1", 10);
  const { url } = useRouteMatch();

  useEffect(() => {
    let isMounted = true;
    setProductsListIsLoading(true);

    UserActions.GetProductsCountByCategoryIdAndCompanyId(
      Number(categoryId),
      Number(companyId)
    ).then((count) => {
      UserActions.GetProductByCategoryIdAndCompanyIdAndPage(
        Number(categoryId),
        Number(companyId),
        0,
        5
      ).then((products) => {
        if (isMounted) {
          if (products !== undefined) {
            setProductsList(products);
          } else {
            setProductsList([]);
          }
          setNumberOfProductsInTotal(count);
          setProductsListIsLoading(false);
        }
      });
    });

    return () => {
      isMounted = false;
    }; // use effect cleanup to set flag false, if unmounted
  }, [categoryId, companyId]);

  const hanglePageChange = (
    event: React.ChangeEvent<unknown>,
    value: number
  ) => {
    setProductsListIsLoading(true);
    UserActions.GetProductByCategoryIdAndCompanyIdAndPage(
      Number(categoryId),
      Number(companyId),
      value - 1,
      5
    ).then((products) => {
      if (products !== undefined) {
        setProductsList(products);
      } else {
        setProductsList([]);
      }

      setProductsListIsLoading(false);
    });
  };

  //Product list component pagintaion
  const productBlockPagination = (
    <Grid item>
      <Pagination
        page={productPage}
        count={
          numberOfProductsInTotal ? Math.ceil(numberOfProductsInTotal / 5) : 0
        }
        onChange={hanglePageChange}
        renderItem={(item) => (
          <PaginationItem
            component={Link}
            to={`${url}/Product${item.page === 1 ? "" : `?page=${item.page}`}`}
            {...item}
          />
        )}
      />
    </Grid>
  );

  return (
    <Grid
      item
      container
      spacing={3}
      justify="center"
      alignItems="center"
      direction="column"
    >
      {numberOfProductsInTotal?(numberOfProductsInTotal > 5 && productBlockPagination):<></>}
      <Grid item container spacing={3} justify="flex-start" direction="row">
        {!productsListIsLoading &&
          productsList &&
          productsList.map((product, i) => (
            <Grid item xs={3} key={i}>
              <Grid item key={product.id}>
                <div
                  className={`${classes.outlinedItem} ${classes.divPointer}`}
                >
                  <Typography variant="h6" gutterBottom noWrap>
                    {product.title}
                  </Typography>
                  <Typography variant="body1" gutterBottom>
                    {"Price: " + product.price}
                  </Typography>
                </div>
              </Grid>
            </Grid>
          ))}
      </Grid>
    </Grid>
  );
};

//|| productsList.length === 0
