import { useState, useEffect, Fragment } from "react";
import Drink from "../Drink/Drink";
import Loading from "../Loading/Loading";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Pagination from "react-bootstrap/Pagination";
import agent from "../../api/agent";
import { useHistory, useLocation } from "react-router-dom";
import "./DrinkList.min.css";

const DrinkList = ({ loading, setLoading }) => {
  const [drinks, setDrinks] = useState([]);
  const [pages, setPages] = useState(0);
  const history = useHistory();

  function useQuery() {
    return new URLSearchParams(useLocation().search);
  }
  let query = useQuery();

  const [page, setPage] = useState(
    query.get("page") != undefined ? query.get("page") : 1
  );

  useEffect(() => {
    setLoading(true);
    try {
      agent.requests
        .get(`/drinks?page=${page}`)
        .then((response) => {
          if (response != null) {
            setDrinks(response.drinks);
            setPages(response.totalPages);
            history.push(`/menu?page=${page}`);
          }
        })
        .then(() => setLoading(false));
    } catch (error) {
      console.log(error);
    }
  }, [page, history, setLoading]);

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
      <Row className="drink-list p-relative justify-content-center">
        {loading === true && <Loading content="Loading Drinks..." />}

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
          <Pagination className="mt-3 justify-content-center">
            {items}
          </Pagination>
        </Col>
      </Row>
    </Fragment>
  );
};

export default DrinkList;
