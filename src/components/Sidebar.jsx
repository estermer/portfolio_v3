import React from 'react';
import { Container, Nav, NavItem, NavLink } from 'reactstrap';

const Sidebar = () => (
  <div className="sidebar">
    <Container>
      <Nav vertical>
        <NavItem>
          <NavLink href="#">Link</NavLink>
        </NavItem>
        <NavItem>
          <NavLink href="#">Link</NavLink>
        </NavItem>
        <NavItem>
          <NavLink href="#">Another Link</NavLink>
        </NavItem>
        <NavItem>
          <NavLink disabled href="#">
            Disabled Link
          </NavLink>
        </NavItem>
      </Nav>
    </Container>
  </div>
);

export default Sidebar;
