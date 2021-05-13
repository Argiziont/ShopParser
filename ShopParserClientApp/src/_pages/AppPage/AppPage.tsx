import React from "react";
import { HashRouter, Route, Switch } from "react-router-dom";
import { HomePage } from "../";

export const AppPage: React.FC = () => {
  return (
    <HashRouter>
      <Switch>
       
        <Route path="/" component={() => <HomePage />}/> 
      </Switch>
    </HashRouter>
  );
};
