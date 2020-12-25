import { useState, useEffect, Fragment } from "react";
import Drink from "../Drink/Drink";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Pagination from "react-bootstrap/Pagination";
import axios from "axios";

import "./DrinkList.min.css";

const DrinkList = () => {
  const [drinks, setDrinks] = useState([]);
  const [pages, setPages] = useState(0);
  const [page, setPage] = useState(1);

  useEffect(() => {
    axios
      .get(`https://localhost:5001/api/drinks?page=${page}`)
      .then((response) => {
        setDrinks(response.data.drinks);
        setPages(response.data.totalPages);
        console.log(response.data.drinks);
      });
  }, [page]);

  let items = [];
  for (let number = 1; number <= pages; number++) {
    items.push(
      <Pagination.Item
        key={number}
        active={number === page}
        onClick={() => setPage(number)}
      >
        {number}
      </Pagination.Item>
    );
  }

  return (
    <Fragment>
      <Row className="drink-list">
        {drinks.map((drink) => (
          <Drink
            name={drink.name}
            key={drink.id}
            id={drink.id}
            ingredients={drink.ingredients}
            image={drink.imageUrl}
            price={drink.price}
          />
        ))}
      </Row>
      <Row>
        <Col>
          <Pagination>{items}</Pagination>
        </Col>
      </Row>
    </Fragment>
  );
};

export default DrinkList;
