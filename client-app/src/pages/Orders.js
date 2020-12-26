import OrdersList from "../layout/OrdersList/OrdersList";
import { useState } from "react";
import Col from "react-bootstrap/Col";
import Card from "react-bootstrap/Card";

const Orders = () => {
  const [loading, setLoading] = useState(true);

  return (
    <Card className="rounded p-4 w-100">
      <Col sm={12}>
        <h1>Orders</h1>
      </Col>
      <Col sm={12} className="p-relative">
        <OrdersList loading={loading} setLoading={setLoading} />
      </Col>
    </Card>
  );
};

export default Orders;
