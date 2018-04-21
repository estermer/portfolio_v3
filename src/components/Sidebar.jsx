import React from 'react';
import { Container, Nav, NavItem, NavLink } from 'reactstrap';
import SiteTitle from './SiteTitle';

const Sidebar = () => (
  <div className="sidebar">
    <Container>
      <SiteTitle />
      <Nav pills vertical>
        <NavItem>
          <NavLink href="#">Link</NavLink>
        </NavItem>
        <NavItem>
          <NavLink href="#">Link</NavLink>
        </NavItem>
        <NavItem>
          <NavLink href="#">Another Link</NavLink>
        </NavItem>
      </Nav>
    </Container>
  </div>
);

export default Sidebar;
