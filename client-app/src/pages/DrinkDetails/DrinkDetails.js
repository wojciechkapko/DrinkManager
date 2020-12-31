import Card from "react-bootstrap/Card";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import Image from "react-bootstrap/Image";
import { useHistory, useLocation } from "react-router-dom";
import { useState, useEffect, Fragment } from "react";
import agent from "../../api/agent";
import Loading from "../../layout/Loading/Loading";
import Reviews from "./Reviews";
import "./DrinkDetails.min.css";
import Stars from "../../layout/Stars/Stars";

const DrinkDetails = () => {
  const location = useLocation();
  const id = location.pathname.split("/")[2];
  const [drink, setDrink] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    try {
      agent.Drink.get(`${id}`)
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
    <Fragment>
      {loading ? (
        <Row className="mx-4 my-5">
          <Loading content="Loading the Drink..." />
        </Row>
      ) : (
        <Fragment>
          <Card className="rounded p-4 drink-details mb-4">
            <Row className="mx-4 my-5">
              <Col
                xs={12}
                lg={5}
                className="justify-content-center align-items-center d-flex mb-5"
              >
                <div className="p-relative">
                  <Image
                    src={drink.imageUrl}
                    roundedCircle
                    className="w-100 shadow-lg"
                  />
                </div>
              </Col>
              <Col xs={0} lg={1}></Col>
              <Col xs={12} lg={6}>
                <Row className="border-bottom pb-4 mb-3" noGutters={true}>
                  <Col>
                    <Stars count={drink.averageReview} />
                    <h1 className="mt-1 mb-3">{drink.name}</h1>
                    <p className="mb-4">{drink.instructions}</p>
                    <div className="d-flex align-items-center">
                      <h3 className="font-weight-bold mr-4 mb-0">
                        ${drink.price}
                      </h3>
                      <Button size="lg" className="ml-auto">
                        Add to Order
                      </Button>
                    </div>
                  </Col>
                </Row>
                <Row className="flex-column">
                  <Col>
                    <h2 className="mb-3">Ingredients</h2>
                    <ul>
                      {drink.ingredients.map((ingredient) => (
                        <li key={ingredient.name}>{ingredient.name}</li>
                      ))}
                    </ul>
                  </Col>
                </Row>
              </Col>
            </Row>
          </Card>
          <Reviews id={id} />
        </Fragment>
      )}
    </Fragment>
  );
};

export default DrinkDetails;
