import logo from "./logo.svg";
import "./App.css";
import React, { useState, useEffect } from "react";
import axios from "axios";
import Notification from "./layout/Notification/Notification";
import Container from "react-bootstrap/Container";
import Menu from "./pages/Menu";
import Orders from "./pages/Orders";
import TopNav from "./layout/Nav/TopNav";
import Home from "./pages/Home";

import { Switch, Route } from "react-router-dom";

const App = () => {
  // const [drinks, setDrinks] = useState([]);

  // useEffect(() => {
  //   axios.get("https://localhost:5001/api/drinks").then((response) => {
  //     setDrinks(response.data);
  //   });
  // }, []);

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
