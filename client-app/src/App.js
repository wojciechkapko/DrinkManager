import logo from "./logo.svg";
import "./App.css";
import "./styles/global.min.css";
import React from "react";
import Container from "react-bootstrap/Container";
import Menu from "./pages/Menu";
import Orders from "./pages/Orders";
import TopNav from "./layout/Nav/TopNav";
import Home from "./pages/Home";

import { Switch, Route } from "react-router-dom";

const App = () => {
  return (
    <Container className="p-3">
      <TopNav />
      <Switch>
        <Route path="/" exact>
          <Home />
        </Route>
        <Route path="/menu">
          <Menu />
        </Route>
        <Route path="/orders">
          <Orders />
        </Route>
      </Switch>
    </Container>
  );
};

export default App;
