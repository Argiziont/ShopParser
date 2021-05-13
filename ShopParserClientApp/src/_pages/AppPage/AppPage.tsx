import React from "react";
import { BrowserRouter as Router, Redirect, Route, Switch } from "react-router-dom";
import { HomePage } from "../";

export const AppPage: React.FC = () => {
  return (
    <Router>
      <Redirect exact from="/" to="Home" />
      <Switch>
        <Route path="/Home" component={() => <HomePage />} />
      </Switch>
    </Router>
  );
};
