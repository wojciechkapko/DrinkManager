import { Form as FinalForm, Field } from "react-final-form";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import agent from "../api/agent";
import Spinner from "react-bootstrap/Spinner";
import Card from "react-bootstrap/Card";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import { toast } from "react-toastify";
import { history } from "..";

const Register = ({ setUser, setisLoggedIn }) => {
  const handleRegisterSubmit = async (values) => {
    try {
      const user = await agent.User.register(values);
      setUser(user);
      setisLoggedIn(true);
      localStorage.setItem("user", JSON.stringify(user));
      localStorage.setItem("token", user.token);
      localStorage.setItem("isLoggedIn", true);
      history.push("/");
    } catch (error) {
      console.log(error);
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
      onSubmit={handleRegisterSubmit}
      render={({ handleSubmit, submitting }) => (
        <Card className="rounded p-4 w-100">
          <Row className="mb-4">
            <Col>
              <h1 className="text-center text-lg-left">Register</h1>
            </Col>
          </Row>
          <Row>
            <Col className="p-relative">
              <Form onSubmit={handleSubmit}>
                <Form.Group controlId="formBasicEmail">
                  <Form.Label>Email address</Form.Label>
                  <Field
                    name="email"
                    placeholder="Enter email"
                    className="form-control"
                    component="input"
                  />
                </Form.Group>
                <Form.Group controlId="formBasicUsername">
                  <Form.Label>Username</Form.Label>
                  <Field
                    name="username"
                    placeholder="Username"
                    className="form-control"
                    component="input"
                  />
                </Form.Group>
                <Form.Group controlId="formBasicPassword">
                  <Form.Label>Password</Form.Label>
                  <Field
                    name="password"
                    placeholder="Password"
                    className="form-control"
                    component="input"
                    type="password"
                  />
                </Form.Group>
                <Button variant="primary" type="submit">
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
              </Form>
            </Col>
          </Row>
        </Card>
      )}
    />
  );
};

export default Register;
