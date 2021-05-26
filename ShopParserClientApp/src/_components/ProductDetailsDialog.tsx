import React from "react";
import {
  createStyles,
  Theme,
  withStyles,
  WithStyles,
} from "@material-ui/core/styles";
import {
  Button,
  Link,
  Dialog,
  IconButton,
  Typography,
} from "@material-ui/core";
import MuiDialogTitle from "@material-ui/core/DialogTitle";
import MuiDialogContent from "@material-ui/core/DialogContent";
import MuiDialogActions from "@material-ui/core/DialogActions";
import CloseIcon from "@material-ui/icons/Close";
import { IProductJson } from "../_actions";

const styles = (theme: Theme) =>
  createStyles({
    root: {
      margin: 0,
      padding: theme.spacing(2),
    },
    closeButton: {
      position: "absolute",
      right: theme.spacing(1),
      top: theme.spacing(1),
      color: theme.palette.grey[500],
    },
  });

interface ProductMuiDialogTitleProps extends WithStyles<typeof styles> {
  id: string;
  children: React.ReactNode;
  onClose: () => void;
}

const ProductMuiDialogTitle = withStyles(styles)(
  (props: ProductMuiDialogTitleProps) => {
    const { children, classes, onClose, ...other } = props;
    return (
      <MuiDialogTitle disableTypography className={classes.root} {...other}>
        <Typography variant="h6">{children}</Typography>
        {onClose ? (
          <IconButton
            aria-label="close"
            className={classes.closeButton}
            onClick={onClose}
          >
            <CloseIcon />
          </IconButton>
        ) : null}
      </MuiDialogTitle>
    );
  }
);

const DialogContent = withStyles((theme: Theme) => ({
  root: {
    padding: theme.spacing(2),
  },
}))(MuiDialogContent);

const DialogActions = withStyles((theme: Theme) => ({
  root: {
    margin: 0,
    padding: theme.spacing(1),
  },
}))(MuiDialogActions);

interface ProductDetailsDialogProps {
  handleSwitch: () => void;
  openState: boolean;
  productInfo: IProductJson;
}

export const ProductDetailsDialog: React.FC<ProductDetailsDialogProps> = ({
  handleSwitch,
  openState,
  productInfo,
}) => {
  const handleExternalUrlClick = (url: string | undefined) => {
    url && window.location.assign(url);
  };

  return (
    productInfo && (
      <div>
        <Dialog
          onClose={handleSwitch}
          aria-labelledby="customized-dialog-title"
          open={openState}
        >
          <ProductMuiDialogTitle
            id="customized-dialog-title"
            onClose={handleSwitch}
          >
            {productInfo.title}
            <Typography variant="body2" gutterBottom>
              {productInfo.stringCategory}
            </Typography>
          </ProductMuiDialogTitle>
          <DialogContent dividers>
            <Typography variant="body1" gutterBottom>
              {productInfo.presence?.title}
            </Typography>
            <Typography variant="body2" gutterBottom>
              {productInfo.scuCode}
            </Typography>
            <Typography variant="h6" gutterBottom>
              {productInfo.price + " " + productInfo.currency}
            </Typography>
            <Typography variant="body1" gutterBottom>
              {productInfo.description}
            </Typography>
            {productInfo.imageUrls?.length === 0 ? (
              <div></div>
            ) : (
              <Typography variant="h6" gutterBottom>
                {"ImageUrls"}
              </Typography>
            )}
            <>
              {productInfo.imageUrls?.map((imgUrl, i) => (
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
              {"Id: " + productInfo.externalId}
            </Typography>
            <Typography variant="body2" gutterBottom>
              {"Sync date: " + productInfo.syncDate}
            </Typography>
          </DialogContent>
          <DialogActions>
            <Button
              autoFocus
              onClick={() => handleExternalUrlClick(productInfo.url)}
              color="primary"
            >
              Open original page
            </Button>
          </DialogActions>
        </Dialog>
      </div>
    )
  );
};
