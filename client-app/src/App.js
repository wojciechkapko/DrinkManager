import "./App.css";
import "./styles/global.min.css";
import Container from "react-bootstrap/Container";
import Menu from "./pages/Menu";
import Orders from "./pages/Orders";
import TopNav from "./layout/Nav/TopNav";
import Login from "./pages/Login";
import Notfound from "./layout/Notfound/Notfound";
import Unauthorized from "./layout/Unauthorized/Unauthorized";
import Home from "./pages/Home";
import { Switch, Route } from "react-router-dom";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import { useState } from "react";
import { ToastContainer } from "react-toastify";

const App = () => {
  const [user, setUser] = useState(
    localStorage.getItem("user") != null
      ? JSON.parse(localStorage.getItem("user"))
      : {}
  );
  const [isLoggedIn, setisLoggedIn] = useState(
    localStorage.getItem("isLoggedIn")
  );

  return (
    <Container className="p-3">
      <ToastContainer position="top-right" />
      <TopNav user={user} isLoggedIn={isLoggedIn} />
      <Row>
        <Col md={12}>
          <Switch>
            <Route path="/" exact>
              <Home />
            </Route>
            <Route path="/menu" component={Menu} />
            <Route path="/orders" component={Orders} />
            <Route path="/login">
              <Login setUser={setUser} setisLoggedIn={setisLoggedIn} />
            </Route>
            <Route path="/unauthorized" component={Unauthorized} />
            <Route component={Notfound} />
          </Switch>
        </Col>
      </Row>
    </Container>
  );
};

export default App;
