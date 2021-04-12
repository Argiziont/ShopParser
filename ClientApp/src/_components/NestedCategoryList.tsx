import React from "react";

//Material UI imports
import ExpandLess from "@material-ui/icons/ExpandLess";
import ExpandMore from "@material-ui/icons/ExpandMore";
import {
  Typography,
  List,
  ListItem,
  ListItemText,
  Collapse,
  IconButton,
  makeStyles,
  createStyles,
} from "@material-ui/core";

//Self project imports
import { IResponseNestedCategory } from "../_actions";

export interface NestedCategoryListProps {
  list: IResponseNestedCategory[];
  padding: number;
  setCurrentShopId: (
    id: number | undefined,
    pagesCount: number | undefined
  ) => Promise<void>;
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
  const [openedCategoryId, setOpenedCategoryId] = React.useState<number>(-1);

  const handleContainedExpandClick = (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    index: number
  ) => {
    event.stopPropagation();
    if (index == openedCategoryId) {
      setOpenedCategoryId(-1);
    } else {
      setOpenedCategoryId(index);
    }
  };

  const handleCategoryClick = (
    id: number | undefined,
    products: string | undefined
  ) => {
    props.setCurrentShopId(id, Number(products));
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
                onClick={() =>
                  handleCategoryClick(element.id, element.productsCount)
                }
              >
                <ListItemText
                  disableTypography
                  primary={
                    <>
                      <Typography variant="body1">{element.name}</Typography>
                      <Typography variant="body2">
                        {"Products updated: " + element.productsCount}
                      </Typography>
                    </>
                  }
                />
              </ListItem>
            ) : (
              <ListItem
                classes={{ root: classes.listItem }}
                button
                onClick={() =>
                  handleCategoryClick(element.id, element.productsCount)
                }
              >
                <ListItemText
                  disableTypography
                  primary={
                    <>
                      <Typography variant="body1">{element.name}</Typography>
                      <Typography variant="body2">
                        {"Products updated: " + element.productsCount}
                      </Typography>
                    </>
                  }
                />
                <IconButton
                  edge="end"
                  onClick={(event) => handleContainedExpandClick(event, i)}
                >
                  {openedCategoryId == i ? <ExpandLess /> : <ExpandMore />}
                </IconButton>
              </ListItem>
            )}
            {element.subCategories == undefined ? (
              <></>
            ) : (
              <Collapse in={openedCategoryId == i} timeout="auto" unmountOnExit>
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
