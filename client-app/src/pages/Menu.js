import DrinkList from "../layout/DrinkList/DrinkList";
import { useState } from "react";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Card from "react-bootstrap/Card";

const Menu = () => {
  const [loading, setLoading] = useState(true);

  return (
    <Card className="rounded p-4 w-100">
      <Row>
        <Col>
          <h1 className="text-center text-lg-left">Menu Items</h1>
        </Col>
      </Row>
      <Row>
        <Col className="p-relative">
          <DrinkList loading={loading} setLoading={setLoading} />
        </Col>
      </Row>
    </Card>
  );
};

export default Menu;
