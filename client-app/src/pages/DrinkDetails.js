import Card from "react-bootstrap/Card";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import Image from "react-bootstrap/Image";
import { useHistory, useLocation } from "react-router-dom";
import { useState, useEffect, Fragment } from "react";
import agent from "../api/agent";
import Loading from "../layout/Loading/Loading";

const DrinkDetails = () => {
  const location = useLocation();
  const id = location.pathname.split("/")[2];
  const [drink, setDrink] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    try {
      agent.Drink.get(`/${id}`)
        .then((response) => {
          if (response != null) {
            // setDrinks(response.drinks);
            // setPages(response.totalPages);
            setDrink(response);
          }
        })
        .then(() => setLoading(false));
    } catch (error) {
      console.log(error);
    }
  }, []);

  return (
    <Card className="rounded p-4 drink-details">
      {loading ? (
        <Loading content="Loading Drinks..." />
      ) : (
        <Fragment>
          <Row className="mx-5 my-5">
            <Col xs={5} className="ml-5 mt-5 mb-4 mr-3">
              <Row className="border-bottom pb-4 mb-3">
                <h1>{drink.name}</h1>
                <p className="mb-4">{drink.instructions}</p>
                <div className="d-flex align-items-center">
                  <h3 className="font-weight-bold mr-4 mb-0">${drink.price}</h3>
                  <Button size="lg">Add to Order</Button>
                </div>
              </Row>
              <Row className="flex-column">
                <h2 className="mb-3">Ingredients</h2>
                <ul>
                  {drink.ingredients.map((ingredient) => (
                    <li key={ingredient.name}>{ingredient.name}</li>
                  ))}
                </ul>
              </Row>
            </Col>
            <Col
              xs={5}
              className="ml-5 mt-5 justify-content-center align-items-center d-flex"
            >
              <div className="p-relative">
                <Image
                  src={drink.imageUrl}
                  roundedCircle
                  className="w-100 shadow-lg"
                />
              </div>
            </Col>
          </Row>
        </Fragment>
      )}
    </Card>
  );
};

export default DrinkDetails;
