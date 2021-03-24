import { Button, Grid, makeStyles, TextField } from "@material-ui/core";
import CloudUploadIcon from "@material-ui/icons/CloudUpload";
import React, { useState } from "react";

import { WebApi } from "../../_services";
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
  const handleGetRequest = async () => {
    try {
      setIsLodaing(true);
     // const response = await WebApi().get(requestUrl);
      //setCartAdverts(response);
      setIsLodaing(false);
    } catch {}
  };

  const dataBlock = isLodaing ? (<div>{"Loading"}</div>) :(<div>{"Loading"}</div>)
    
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
        <Grid container xs={4}></Grid>
        <Grid
          container
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
              onClick={handleGetRequest}
            >
              {"Submit"}
            </Button>
          </Grid>
        </Grid>
        <Grid container xs={4}></Grid>
      </Grid>
      {dataBlock}
    </React.Fragment>
  );
};
