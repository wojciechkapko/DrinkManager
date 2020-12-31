import { Pagination as PaginationBootstrap, Form } from "react-bootstrap";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import "./Pagination.min.css";

const Pagination = ({ page, pages, pageCount, setPage, setPageCount }) => {
  let items = [];
  let pageCountDefault = process.env.REACT_APP_DEFAULT_PAGE_NUMBER;
  let pageCountLow = process.env.REACT_APP_DEFAULT_PAGE_NUMBER_LOW;
  let pageCountHigh = process.env.REACT_APP_DEFAULT_PAGE_NUMBER_HIGH;

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
      <Col
        xs={12}
        md={6}
        lg={9}
        className="d-flex justify-content-center justify-content-md-end"
      >
        <PaginationBootstrap className="mt-3 justify-content-center">
          {items}
        </PaginationBootstrap>
      </Col>
      <Col
        xs={8}
        md={6}
        lg={3}
        className="align-items-center d-flex justify-content-center justify-content-md-end px-5 px-md-3 mt-2 mt-md-0 mx-auto mx-md-0 flex-column flex-md-row"
      >
        <span className="text-nowrap mr-2">Per page</span>
        <Form.Control
          as="select"
          onChange={(e) => setPageCount(e.target.value)}
          value={pageCount}
          className="accent-light mt-2 mt-md-0"
        >
          <option value={pageCountLow}>{pageCountLow}</option>
          <option value={pageCountDefault}>{pageCountDefault}</option>
          <option value={pageCountHigh}>{pageCountHigh}</option>
        </Form.Control>
      </Col>
    </Row>
  );
};

export default Pagination;
