import DrinkList from "../layout/DrinkList/DrinkList";
import { useState } from "react";
import Col from "react-bootstrap/Col";
import Card from "react-bootstrap/Card";

const Menu = () => {
  const [loading, setLoading] = useState(true);

  return (
    <Card className="rounded p-4 w-100">
      <Col sm={12}>
        <h1>Menu Items</h1>
      </Col>
      <Col sm={12} className="p-relative">
        <DrinkList loading={loading} setLoading={setLoading} />
      </Col>
    </Card>
  );
};

export default Menu;
