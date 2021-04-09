import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Avatar from "@material-ui/core/Avatar";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemAvatar from "@material-ui/core/ListItemAvatar";
import ListItemText from "@material-ui/core/ListItemText";
import DialogTitle from "@material-ui/core/DialogTitle";
import Dialog from "@material-ui/core/Dialog";
import LocalOfferIcon from "@material-ui/icons/LocalOffer";
import { blue } from "@material-ui/core/colors";

export interface SelectSortTypeDialogProps {
  open: boolean;
  selectedValue: string;
  onClose: (value: string) => void;
}
export const SortTypes = ["Shops", "Categories"];
const useStyles = makeStyles({
  avatar: {
    backgroundColor: blue[100],
    color: blue[600],
  },
});
export const SelectSortTypeDialog: React.FC<SelectSortTypeDialogProps> = (
  props: SelectSortTypeDialogProps
) => {
  const classes = useStyles();
  const { onClose, selectedValue, open } = props;

  const handleClose = () => {
    onClose(selectedValue);
  };

  const handleListItemClick = (value: string) => {
    onClose(value);
  };

  return (
    <Dialog
      onClose={handleClose}
      aria-labelledby="simple-dialog-title"
      open={open}
    >
      <DialogTitle id="simple-dialog-title">Sort By</DialogTitle>
      <List>
        {SortTypes.map((sortType) => (
          <ListItem
            button
            onClick={() => handleListItemClick(sortType)}
            key={sortType}
          >
            <ListItemAvatar>
              <Avatar className={classes.avatar}>
                <LocalOfferIcon fontSize="default" />
              </Avatar>
            </ListItemAvatar>
            <ListItemText primary={sortType} />
          </ListItem>
        ))}
      </List>
    </Dialog>
  );
};
