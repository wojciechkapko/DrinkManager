import { Fragment } from "react";
import "./StarsHover.min.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import { Field } from "react-final-form";

const StarsHover = () => {
  return (
    <Fragment>
      <div class="rating" style={{ width: 200 }}>
        <Field
          id="rating-5"
          name="reviewscore"
          component="input"
          type="radio"
          value="5"
        />
        <label for="rating-5">
          <FontAwesomeIcon icon={faStar} size="lg" className="mr-1" />
        </label>
        <Field
          id="rating-4"
          name="reviewscore"
          component="input"
          type="radio"
          value="4"
        />
        <label for="rating-4">
          <FontAwesomeIcon icon={faStar} size="lg" className="mr-1" />
        </label>
        <Field
          id="rating-3"
          name="reviewscore"
          component="input"
          type="radio"
          value="3"
        />
        <label for="rating-3">
          <FontAwesomeIcon icon={faStar} size="lg" className="mr-1" />
        </label>
        <Field
          id="rating-2"
          name="reviewscore"
          component="input"
          type="radio"
          value="2"
        />
        <label for="rating-2">
          <FontAwesomeIcon icon={faStar} size="lg" className="mr-1" />
        </label>
        <Field
          id="rating-1"
          name="reviewscore"
          component="input"
          type="radio"
          value="1"
        />
        <label for="rating-1">
          <FontAwesomeIcon icon={faStar} size="lg" className="mr-1" />
        </label>
      </div>
    </Fragment>
  );
};

export default StarsHover;
