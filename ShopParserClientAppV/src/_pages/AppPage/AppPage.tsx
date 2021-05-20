import React from "react";
import { BrowserRouter , Route, Switch } from "react-router-dom";
import { HomePage } from "../";

export const AppPage: React.FC = () => {
  return (
    <BrowserRouter>
      <Switch>
        <Route path="/" component={() => <HomePage />} />
      </Switch>
    </BrowserRouter>
  );
};
