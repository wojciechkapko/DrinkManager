import OrdersList from "../layout/OrdersList/OrdersList";
import { useState } from "react";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import Card from "react-bootstrap/Card";

const Orders = () => {
  const [loading, setLoading] = useState(true);

  return (
    <Card className="rounded p-4 w-100">
      <Row>
        <Col>
          <h1>Orders</h1>
        </Col>
      </Row>
      <Row>
        <Col className="p-relative">
          <OrdersList loading={loading} setLoading={setLoading} />
        </Col>
      </Row>
    </Card>
  );
};

export default Orders;
