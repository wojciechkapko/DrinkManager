import Dashboard from "./Dashboard";
import Users from "./Users";
import { Switch, Route } from "react-router-dom";
import SideNav from "../../layout/Nav/SideNav";
import Card from "react-bootstrap/Card";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

const AdminHome = () => {
  return (
    <Row>
      <Col xs={12} md={"auto"}>
        <SideNav />
      </Col>
      <Col xs={12} md={true}>
        <Card className="rounded p-4">
          <Switch>
            <Route path="/manager" exact>
              <Dashboard />
            </Route>
            <Route path="/manager/users">
              <Users />
            </Route>
          </Switch>
        </Card>
      </Col>
    </Row>
  );
};

export default AdminHome;
