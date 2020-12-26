import Navbar from "react-bootstrap/Navbar";
import Nav from "react-bootstrap/Nav";
import { Link, NavLink } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCocktail, faUserCircle } from "@fortawesome/free-solid-svg-icons";
import { Fragment } from "react";

const TopNav = ({ user, isLoggedIn }) => {
  return (
    <Navbar bg="light" expand="lg" className="p-4 mb-4 rounded">
      <Navbar.Brand as={Link} to="/" className="pr-3 mr-2 border-right">
        <FontAwesomeIcon icon={faCocktail} size="lg" className="mr-2" />
        Drinkland
      </Navbar.Brand>
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav>
          <NavLink to="/" activeClassName="active" className="nav-link" exact>
            Home
          </NavLink>
          <NavLink to="/menu" activeClassName="active" className="nav-link">
            Menu
          </NavLink>
          {isLoggedIn && user && (
            <NavLink to="/orders" activeClassName="active" className="nav-link">
              Orders
            </NavLink>
          )}
        </Nav>
        <div className="ml-auto d-flex align-items-center">
          {isLoggedIn && user ? (
            <Fragment>
              <NavLink
                to="/manager"
                activeClassName="active"
                className="nav-link"
              >
                Manager Panel
              </NavLink>
              <span className="ml-2">
                <FontAwesomeIcon
                  icon={faUserCircle}
                  size="lg"
                  className="mr-2"
                />
                <strong>{user.username}</strong>
              </span>
            </Fragment>
          ) : (
            <Fragment>
              <NavLink
                to="/login"
                activeClassName="active"
                className="nav-link"
              >
                Login
              </NavLink>
              <NavLink
                to="/register"
                activeClassName="active"
                className="nav-link"
              >
                Register
              </NavLink>
            </Fragment>
          )}
        </div>
      </Navbar.Collapse>
    </Navbar>
  );
};

export default TopNav;
