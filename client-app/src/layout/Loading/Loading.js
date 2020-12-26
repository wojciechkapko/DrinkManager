import Spinner from "react-bootstrap/Spinner";

const Loading = ({ content, size }) => {
  return (
    <div className="d-flex flex-column justify-content-center align-items-center loader p-4 rounded shadow">
      <Spinner animation="border" variant="danger" size={size} />
      <p className="mt-2 mb-0 text-white">{content}</p>
    </div>
  );
};

export default Loading;
