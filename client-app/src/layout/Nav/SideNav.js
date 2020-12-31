import Nav from "react-bootstrap/Nav";
import { NavLink } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faChartBar, faUsers } from "@fortawesome/free-solid-svg-icons";
import { Fragment } from "react";

const SideNav = () => {
  return (
    <Nav className="p-4 mb-4 rounded bg-white flex-row flex-md-column">
      <NavLink
        to="/manager"
        activeClassName="active"
        className="nav-link"
        exact
      >
        <FontAwesomeIcon icon={faChartBar} size="lg" className="mr-2" />
        Dashboard
      </NavLink>
      <NavLink
        to="/manager/users"
        activeClassName="active"
        className="nav-link"
      >
        <FontAwesomeIcon icon={faUsers} size="lg" className="mr-2" />
        Users
      </NavLink>
    </Nav>
  );
};

export default SideNav;
