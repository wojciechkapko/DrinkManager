import logo from "./logo.svg";
import "./App.css";
import "./styles/global.min.css";
import Container from "react-bootstrap/Container";
import Menu from "./pages/Menu";
import Orders from "./pages/Orders";
import TopNav from "./layout/Nav/TopNav";
import Home from "./pages/Home";
import { Switch, Route } from "react-router-dom";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

const App = () => {
  return (
    <Container className="p-3">
      <TopNav />
      <Row>
        <Col md={12}>
          <Switch>
            <Route path="/" exact>
              <Home />
            </Route>
            <Route path="/menu">
              <Menu />
            </Route>
            <Route path="/orders">
              <Orders />
            </Route>
          </Switch>
        </Col>
      </Row>
    </Container>
  );
};

export default App;
