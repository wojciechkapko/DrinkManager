import React, { useEffect } from "react";
import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";

const Drink = ({ name, id, ingredients, image }) => {
  useEffect(() => {
    console.log(image);
  }, []);

  return (
    <Card style={{ width: "15rem" }} className="rounded m-3">
      <Card.Img variant="top" src={image} />
      <Card.Body>
        <Card.Title>{name}</Card.Title>
        <Card.Text>
          {ingredients.map((ingredient) => (
            <span>{ingredient.name}</span>
          ))}
        </Card.Text>
        <Button variant="primary">Go somewhere</Button>
      </Card.Body>
    </Card>
  );
};

export default Drink;
