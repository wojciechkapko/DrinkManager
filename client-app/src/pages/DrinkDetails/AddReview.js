import { Form as FinalForm, Field } from "react-final-form";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import agent from "../../api/agent";
import Spinner from "react-bootstrap/Spinner";
import Card from "react-bootstrap/Card";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import { toast } from "react-toastify";
import StarsHover from "../../layout/Stars/StarsHover";

const AddReview = ({ id, reviews, setReviews }) => {
  const handleLoginSubmit = async (values) => {
    try {
      console.log(values);
      const review = await agent.Reviews.post(id, values);
      setReviews([...reviews, review]);
      toast.success("Review added");
    } catch (error) {
      if (typeof error.data == "string") {
        toast.error(`${error.data}`);
      } else {
        let errors = error.data.errors;
        for (const property in errors) {
          toast.error(`${error.data.errors[property]}`);
        }
      }
    }
  };
  return (
    <FinalForm
      onSubmit={handleLoginSubmit}
      render={({ handleSubmit, submitting }) => (
        <Card className="rounded p-4 mb-4">
          <Row className="mb-4">
            <Col>
              <h1>Add your review</h1>
            </Col>
          </Row>
          <Row>
            <Col className="p-relative">
              <Form onSubmit={handleSubmit}>
                <Form.Group controlId="authorName">
                  <Form.Label>Name</Form.Label>
                  <Field
                    name="authorname"
                    placeholder="Name"
                    className="form-control"
                    component="input"
                  />
                </Form.Group>
                <Form.Group controlId="reviewText">
                  <Form.Label>Your review</Form.Label>
                  <Field
                    name="reviewtext"
                    placeholder="Enter review"
                    className="form-control"
                    component="textarea"
                  />
                </Form.Group>
                <Form.Group controlId="reviewscore">
                  <Form.Label>Your score</Form.Label>
                  <StarsHover />
                </Form.Group>

                <div className="d-flex justify-content-center">
                  <Button
                    variant="primary"
                    type="submit"
                    size="lg"
                    className="d-flex align-items-center justify-content-center"
                  >
                    {submitting == true ? (
                      <Spinner
                        as="span"
                        animation="border"
                        size="sm"
                        role="status"
                        aria-hidden="true"
                      />
                    ) : (
                      <span>Submit</span>
                    )}
                  </Button>
                </div>
              </Form>
            </Col>
          </Row>
        </Card>
      )}
    />
  );
};

export default AddReview;
