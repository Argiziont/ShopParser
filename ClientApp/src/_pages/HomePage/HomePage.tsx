import { Button, Grid, makeStyles, TextField } from "@material-ui/core";
import CloudUploadIcon from "@material-ui/icons/CloudUpload";
import React, { useState } from "react";

import { UserActions } from "../../_actions";
import { ParseDataSegment } from "../../_components";
import { IResponseShop } from "../../_actions";

const useStyles = makeStyles((theme) => ({
  rootBox: {
    marginTop: theme.spacing(5),
    marginBottom: theme.spacing(15),
  },
  urlField: {
    margin: theme.spacing(5),
  },
}));
export const HomePage: React.FC = () => {
  const [requestUrl, setRequestUrl] = useState<string>(
    "https://prom.ua/Sportivnye-kostyumy"
  );
  const [isLodaing, setIsLodaing] = useState<boolean>(false);
  const [shopsList, setShopsList] = useState<IResponseShop[]>();

  const classes = useStyles();

  const handleRequestUrlChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRequestUrl(event.target.value);
  };
  const handleGetShopsRequest = async () => {
    try {
      setIsLodaing(true);
      const response = await UserActions.GetAllShops();
      if (response != undefined) {
        setShopsList(response);
        console.log(response);
      }

      setIsLodaing(false);
    } catch {}
  };

  const dataBlock = isLodaing ? (
    <div>{"Loading"}</div>
  ) : (
    <ParseDataSegment ShopList={shopsList}></ParseDataSegment>
  );

  // cartAdverts?.map((cartAdvert,i) => {
  // return <ParseDataSegment
  //   advertId={cartAdvert.advertId}
  //   companyName={cartAdvert.companyName}
  //   currency={cartAdvert.currency}
  //   description={cartAdvert.description}
  //   fullCurrency={cartAdvert.fullCurrency}
  //   fullPrice={cartAdvert.fullPrice}
  //   imageUrls={cartAdvert.imageUrls}
  //   key={i}
  //   optCurrency={cartAdvert.optCurrency}
  //   optPrice={cartAdvert.optPrice}
  //   positivePercent={cartAdvert.positivePercent}
  //   presence={cartAdvert.presence}
  //   price={cartAdvert.price}
  //   ratingsPerLastYear={cartAdvert.ratingsPerLastYear}
  //   scuCode={cartAdvert.scuCode}
  //   title={cartAdvert.title}
  // />;
  //}
  // );
  return (
    <React.Fragment>
      <Grid
        container
        spacing={2}
        direction="row"
        justify="center"
        className={classes.rootBox}
      >
        <Grid container item xs={4}></Grid>
        <Grid
          container
          item
          xs={4}
          spacing={2}
          justify="center"
          alignItems="flex-end"
        >
          <Grid item>
            <TextField
              label="Site URL"
              variant="standard"
              value={requestUrl}
              onChange={handleRequestUrlChange}
            />
          </Grid>
          <Grid item>
            <Button
              variant="contained"
              endIcon={<CloudUploadIcon />}
              onClick={handleGetShopsRequest}
            >
              {"Submit"}
            </Button>
          </Grid>
        </Grid>
        <Grid item xs={4}></Grid>
      </Grid>
      {dataBlock}
    </React.Fragment>
  );
};
