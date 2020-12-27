import { Pagination as PaginationBootstrap, Form } from "react-bootstrap";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import "./Pagination.min.css";

const Pagination = ({ page, pages, pageCount, setPage, setPageCount }) => {
  let items = [];

  for (let number = 1; number <= pages; number++) {
    items.push(
      <PaginationBootstrap.Item
        key={number}
        active={number === page}
        onClick={() => setPage(number)}
      >
        {number}
      </PaginationBootstrap.Item>
    );
  }
  return (
    <Row>
      <Col className="d-flex justify-content-end">
        <PaginationBootstrap className="mt-3 justify-content-center">
          {items}
        </PaginationBootstrap>
      </Col>
      <Col xs={3} className="align-items-center d-flex">
        <span className="text-nowrap mr-2">Per page</span>
        <Form.Control
          as="select"
          onChange={(e) => setPageCount(e.target.value)}
          value={pageCount}
          className="accent-light"
        >
          <option value={5}>5</option>
          <option value={10}>10</option>
          <option value={25}>25</option>
          <option value={50}>50</option>
        </Form.Control>
      </Col>
    </Row>
  );
};

export default Pagination;
