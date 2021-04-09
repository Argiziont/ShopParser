import React from "react";
import { makeStyles, Theme, createStyles } from "@material-ui/core/styles";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemText from "@material-ui/core/ListItemText";
import Collapse from "@material-ui/core/Collapse";
import ExpandLess from "@material-ui/icons/ExpandLess";
import ExpandMore from "@material-ui/icons/ExpandMore";
import { IResponseNestedCategory } from "../_actions";

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      width: "100%",
      maxWidth: 360,
    },
  })
);
export interface NestedCategoryListProps {
  list: IResponseNestedCategory[];
  padding: number;
}
export const NestedCategoryList: React.FC<NestedCategoryListProps> = (
  props: NestedCategoryListProps
) => {
  const classes = useStyles();
  const [open, setOpen] = React.useState<number>(-1);

  const handleContainedExpandClick = (id: number) => {
    if (id == open) {
      setOpen(-1);
    } else {
      setOpen(id);
    }
  };

  return (
    <List
      style={{
        paddingLeft: props.padding + "px",
      }}
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
              <ListItem button>
                <ListItemText primary={element.name} />
              </ListItem>
            ) : (
              <ListItem button onClick={() => handleContainedExpandClick(i)}>
                <ListItemText primary={element.name} />
                {open == i ? <ExpandLess /> : <ExpandMore />}
              </ListItem>
            )}
            {element.subCategories == undefined ? (
              <></>
            ) : (
              <Collapse in={open == i} timeout="auto" unmountOnExit>
                <NestedCategoryList
                  list={element.subCategories}
                  padding={props.padding + 2}
                ></NestedCategoryList>
              </Collapse>
            )}
          </div>
        );
      })}
    </List>
  );
};
