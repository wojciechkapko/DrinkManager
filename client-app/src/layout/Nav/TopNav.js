import React from "react";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";
import Nav from "react-bootstrap/Nav";
import { Link, NavLink } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCocktail } from "@fortawesome/free-solid-svg-icons";

const TopNav = () => {
  return (
    <Navbar bg="light" expand="lg" className="p-4">
      <Navbar.Brand as={Link} to="/" className="pr-3 mr-2 border-right">
        <FontAwesomeIcon icon={faCocktail} size="lg" className="mr-2" />{" "}
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
          <NavLink to="/orders" activeClassName="active" className="nav-link">
            Orders
          </NavLink>
        </Nav>
        <NavDropdown title="Dropdown" id="basic-nav-dropdown">
          <NavDropdown.Item href="#action/3.1">Action</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.2">Another action</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.3">Something</NavDropdown.Item>
          <NavDropdown.Divider />
          <NavDropdown.Item href="#action/3.4">Separated link</NavDropdown.Item>
        </NavDropdown>
      </Navbar.Collapse>
    </Navbar>
  );
};

export default TopNav;
