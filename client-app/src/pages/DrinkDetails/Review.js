import { Fragment } from "react";
import Stars from "../../layout/Stars/Stars";
import "./Review.min.css";

const Review = ({ review }) => {
  const date = new Date(review.reviewDate).toLocaleDateString("en-GB");
  return (
    <Fragment>
      <div className="review mb-2 pb-2 border-bottom p-relative">
        <p className="d-flex">
          <span className="score">
            <Stars count={review.reviewScore} />
          </span>
        </p>

        <p>{review.reviewText}</p>
        <small>
          Date: {date} Author: {review.authorName}
        </small>
      </div>
    </Fragment>
  );
};

export default Review;
