import { Fragment } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar } from "@fortawesome/free-solid-svg-icons";

const Stars = ({ count }) => {
  const stars = [];
  for (let i = 0; i < count; i++) {
    stars.push(
      <FontAwesomeIcon
        key={i}
        icon={faStar}
        size="lg"
        className="mr-1"
        color="#F3CF44"
      />
    );
  }
  return <Fragment>{stars}</Fragment>;
};

export default Stars;
