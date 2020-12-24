import React, { useState, useEffect, Fragment } from "react";
import axios from "axios";
import DrinkList from "../layout/DrinkList/DrinkList";

const Menu = () => {
  const [drinks, setDrinks] = useState([]);

  useEffect(() => {
    axios.get("https://localhost:5001/api/drinks").then((response) => {
      setDrinks(response.data);
    });
  }, []);
  return (
    <Fragment>
      <h1>Menu</h1>
      <DrinkList drinks={drinks} />
    </Fragment>
  );
};

export default Menu;
