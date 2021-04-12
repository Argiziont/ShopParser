import React from "react";

//Material UI imports
import {
  Dialog,
  DialogTitle,
  ListItemText,
  ListItemAvatar,
  ListItem,
  List,
  Avatar,
  makeStyles,
} from "@material-ui/core";
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
