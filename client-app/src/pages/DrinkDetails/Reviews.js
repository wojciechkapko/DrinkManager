import Card from "react-bootstrap/Card";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import Image from "react-bootstrap/Image";
import { useHistory, useLocation } from "react-router-dom";
import { useState, useEffect, Fragment } from "react";
import agent from "../../api/agent";
import Loading from "../../layout/Loading/Loading";
import "./DrinkDetails.min.css";
import Review from "./Review";

const Reviews = ({ id }) => {
  const [reviews, setReviews] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    try {
      agent.Reviews.get(`${id}`)
        .then((response) => {
          if (response != null) {
            setReviews(response.reviews);
            // setPages(response.totalPages);
          }
        })
        .then(() => setLoading(false));
    } catch (error) {
      console.log(error);
    }
  }, []);

  return (
    <Card className="rounded p-5 drink-details">
      {loading ? (
        <Loading content="Loading Reviews..." />
      ) : (
        <Fragment>
          <Row className="mx-5">
            <Col xs={12} className="mt-5 mb-4">
              <Row className="border-bottom pb-2 mb-3">
                <h1>Reviews</h1>
              </Row>
              <Row>
                <Col sm={12}>
                  {reviews.length > 0 ? (
                    reviews.map((review) => (
                      <Review key={review.id} review={review} />
                    ))
                  ) : (
                    <p>No reviews yet</p>
                  )}
                </Col>
              </Row>
            </Col>
          </Row>
        </Fragment>
      )}
    </Card>
  );
};

export default Reviews;
