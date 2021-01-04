import { useState, useEffect } from "react";
import agent from "../../api/agent";
import Loading from "../../layout/Loading/Loading";
import Pagination from "../../layout/Pagination/Pagination";
import Table from "react-bootstrap/Table";
import { useHistory, useLocation } from "react-router-dom";

const Users = () => {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);

  const [pages, setPages] = useState(0);
  const history = useHistory();

  function useQuery() {
    return new URLSearchParams(useLocation().search);
  }
  let query = useQuery();

  const [page, setPage] = useState(
    query.get("page") !== null ? query.get("page") : 1
  );
  const [pageCount, setPageCount] = useState(
    query.get("pageCount") !== null ? query.get("pageCount") : 10
  );

  useEffect(() => {
    setLoading(true);
    agent.requests
      .get(`/admin/users?page=${page}&pageCount=${pageCount}`)
      .then((response) => {
        if (response != null) {
          setUsers(response.users);
          setPages(response.totalPages);
          history.push(`/manager/users?page=${page}&pageCount=${pageCount}`);
        }
      })
      .then(() => setLoading(false));
  }, [page, history, setLoading, pageCount, setPageCount]);

  return (
    <div>
      <h1 className="text-center text-lg-left mb-3">Users</h1>
      {loading === true && <Loading content="Loading Users..." />}

      <Table responsive hover>
        <thead>
          <tr border="0">
            <th>Username</th>
            <th>Email</th>
            <th>Role</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr key={user.id}>
              <td>{user.username}</td>
              <td>{user.email}</td>
              <td>{user.role}</td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Pagination
        page={page}
        pages={pages}
        setPage={setPage}
        pageCount={pageCount}
        setPageCount={setPageCount}
      />
    </div>
  );
};

export default Users;
