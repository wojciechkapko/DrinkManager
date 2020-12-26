import { useState, useEffect, Fragment } from "react";
import Loading from "../Loading/Loading";
import Row from "react-bootstrap/Row";
import agent from "../../api/agent";

const OrdersList = ({ loading, setLoading }) => {
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    setLoading(true);
    agent.requests
      .get(`/orders`)
      .then((response) => {
        if (response != null) {
          setOrders(response);
        }
      })
      .then(() => setLoading(false));
  }, [setLoading]);

  return (
    <Fragment>
      <Row className="drink-list p-relative justify-content-center">
        {loading === true && <Loading content="Loading Orders..." />}

        {orders.map((order) => (
          <p key={order}>{order}</p>
        ))}
      </Row>
    </Fragment>
  );
};

export default OrdersList;
