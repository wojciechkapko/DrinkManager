import logo from "./logo.svg";
import "./App.css";
import React, { useState, useEffect, Fragment } from "react";
import axios from "axios";

const App = () => {
  const [drinks, setDrinks] = useState([]);

  useEffect(() => {
    axios.get("https://localhost:5001/api/drinks").then((response) => {
      setDrinks(response.data.drinks);
    });
  }, []);

  return (
    <Fragment>
      <header className="App-header">
        <p>{drinks.map((drink) => drink.name)}</p>
      </header>
    </Fragment>
  );
};

export default App;
