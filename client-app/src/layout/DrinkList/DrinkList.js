import { useState, useEffect, Fragment } from "react";
import Drink from "../Drink/Drink";
import Loading from "../Loading/Loading";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Pagination from "../Pagination/Pagination";
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
  const [pageCount, setPageCount] = useState(
    query.get("pageCount") != undefined
      ? query.get("pageCount")
      : process.env.REACT_APP_DEFAULT_PAGE_NUMBER
  );

  useEffect(() => {
    setLoading(true);
    try {
      agent.requests
        .get(`/drinks?page=${page}&pageCount=${pageCount}`)
        .then((response) => {
          if (response != null) {
            setDrinks(response.drinks);
            setPages(response.totalPages);
            history.push(`/menu?page=${page}&pageCount=${pageCount}`);
          }
        })
        .then(() => setLoading(false));
    } catch (error) {
      console.log(error);
    }
  }, [page, history, setLoading, pageCount, setPageCount]);

  return (
    <Fragment>
      <Row className="drink-list p-relative justify-content-center justify-content-xl-start">
        {loading === true && <Loading content="Loading Drinks..." />}

        {drinks.map((drink) => (
          <Drink
            name={drink.name}
            key={drink.id}
            id={drink.id}
            ingredients={drink.ingredients}
            image={drink.imageUrl}
            price={drink.price}
            averageReview={drink.averageReview}
          />
        ))}
      </Row>
      <Pagination
        page={page}
        pages={pages}
        setPage={setPage}
        pageCount={pageCount}
        setPageCount={setPageCount}
      />
    </Fragment>
  );
};

export default DrinkList;
