import React from "react";
import { makeStyles, createStyles } from "@material-ui/core/styles";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemText from "@material-ui/core/ListItemText";
import Collapse from "@material-ui/core/Collapse";
import ExpandLess from "@material-ui/icons/ExpandLess";
import ExpandMore from "@material-ui/icons/ExpandMore";
import { IResponseNestedCategory } from "../_actions";
import IconButton from "@material-ui/core/IconButton";

export interface NestedCategoryListProps {
  list: IResponseNestedCategory[];
  padding: number;
  setCurrentShopId: (id: number | undefined) => void;
}
export const NestedCategoryList: React.FC<NestedCategoryListProps> = (
  props: NestedCategoryListProps
) => {
  const useStyles = makeStyles(() =>
    createStyles({
      root: {
        width: "100%",
        maxWidth: 360,
      },
      categoryItem: {
        maxWidth: "320px",
        minWidth: "300px",
        background: "#D3D3D3",
        border: 0,
        borderRadius: 16,
        padding: "15px 15px",
      },
      listItem: {
        paddingLeft: props.padding + "px",
        paddingTop: "5px",
        paddingBottom: "5px",
      },
    })
  );
  const classes = useStyles();
  const [open, setOpen] = React.useState<number>(-1);

  const handleContainedExpandClick = (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    index: number
  ) => {
    event.stopPropagation();
    if (index == open) {
      setOpen(-1);
    } else {
      setOpen(index);
    }
  };

  const handleCategoryClick = (id: number | undefined) => {
    console.log(id);
    props.setCurrentShopId(id);
  };

  return (
    <List
      component="nav"
      aria-labelledby="nested-list-subheader"
      className={classes.root}
    >
      {props.list.map((element, i) => {
        return (
          <div key={i}>
            {element.subCategories == undefined ? (
              <></>
            ) : element.subCategories.length === 0 ? (
              <ListItem
                classes={{ root: classes.listItem }}
                button
                onClick={() => handleCategoryClick(element.id)}
              >
                <ListItemText primary={element.name} />
              </ListItem>
            ) : (
              <ListItem
                classes={{ root: classes.listItem }}
                button
                onClick={() => handleCategoryClick(element.id)}
              >
                <ListItemText primary={element.name} />
                <IconButton
                  edge="end"
                  onClick={(event) => handleContainedExpandClick(event, i)}
                >
                  {open == i ? <ExpandLess /> : <ExpandMore />}
                </IconButton>
              </ListItem>
            )}
            {element.subCategories == undefined ? (
              <></>
            ) : (
              <Collapse in={open == i} timeout="auto" unmountOnExit>
                <NestedCategoryList
                  setCurrentShopId={props.setCurrentShopId}
                  list={element.subCategories}
                  padding={props.padding + 10}
                ></NestedCategoryList>
              </Collapse>
            )}
          </div>
        );
      })}
    </List>
  );
};
//setCurrentShopId
