import {  Color } from "@material-ui/lab/Alert";
export interface SnackbarMessage {
    message: string;
    type: Color;
    key: number;
  }
  
  export interface SnackbarState {
    open: boolean;
    snackPack: SnackbarMessage[];
    messageInfo?: SnackbarMessage;
  }