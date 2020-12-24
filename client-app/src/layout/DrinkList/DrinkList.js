import React from "react";
import Drink from "../Drink/Drink";
import Row from "react-bootstrap/Row";
import "./DrinkList.min.css";

const DrinkList = ({ drinks }) => {
  return (
    <Row className="drink-list">
      {drinks.map((drink) => (
        <Drink
          name={drink.name}
          key={drink.id}
          id={drink.id}
          ingredients={drink.ingredients}
          image={drink.imageUrl}
        />
      ))}
    </Row>
  );
};

export default DrinkList;
