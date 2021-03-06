import Card from "react-bootstrap/Card";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";
import Stars from "../../layout/Stars/Stars";

const Drink = ({ name, id, ingredients, image, price, averageReview }) => {
  const cardStyle = {
    width: "240px",
    margin: "6rem 1rem 1rem 1rem",
    paddingTop: "7.5rem",
  };

  return (
    <Card style={cardStyle} className="rounded" id={id}>
      <Link to={`/drinkdetails/${id}`}>
        <Card.Img src={image} className="shadow" />
      </Link>
      <Card.Body className="d-flex flex-column">
        <Link to={`/drinkdetails/${id}`}>
          <p>
            <Stars count={averageReview} />
          </p>
          <Card.Title>{name}</Card.Title>
        </Link>
        <Card.Text className="text-muted pr-4">
          {ingredients.map((ingredient) => ingredient.name).join(", ")}
        </Card.Text>
        <h4 className="font-weight-bold m-0 mt-auto">${price}</h4>
        <button className="add-drink p-3 border-0 shadow d-flex align-items-center justify-content-center">
          <FontAwesomeIcon icon={faPlus} size="sm" />
        </button>
      </Card.Body>
    </Card>
  );
};

export default Drink;
