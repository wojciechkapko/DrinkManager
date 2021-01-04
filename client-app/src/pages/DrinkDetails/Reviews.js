import Card from "react-bootstrap/Card";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import { useState, useEffect, Fragment } from "react";
import agent from "../../api/agent";
import Loading from "../../layout/Loading/Loading";
import "./DrinkDetails.min.css";
import Review from "./Review";
import AddReview from "./AddReview";

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
  }, [setReviews, id]);

  return (
    <Row>
      <Col xs={12} md={6}>
        <AddReview id={id} setReviews={setReviews} reviews={reviews} />
      </Col>
      <Col xs={12} md={6}>
        <Card className="rounded p-4 drink-details">
          {loading ? (
            <Loading content="Loading Reviews..." />
          ) : (
            <Fragment>
              <Row className="mx-1">
                <Col xs={12} className="mb-3">
                  <h1 className="text-center text-lg-left border-bottom pb-2 mb-3">
                    Reviews
                  </h1>
                  <Row>
                    <Col sm={12}>
                      {reviews.length > 0 ? (
                        reviews.map((review) => (
                          <Review key={review.id} review={review} />
                        ))
                      ) : (
                        <p className="text-center text-lg-left">
                          No reviews yet
                        </p>
                      )}
                    </Col>
                  </Row>
                </Col>
              </Row>
            </Fragment>
          )}
        </Card>
      </Col>
    </Row>
  );
};

export default Reviews;
