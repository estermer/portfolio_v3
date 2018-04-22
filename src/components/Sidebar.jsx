import React from 'react';
import PropTypes from 'prop-types';
import { Container, Nav } from 'reactstrap';
import SiteTitle from './SiteTitle';
import NavButton from './NavButton';

const Sidebar = ({ onClickNavItem, activeNavItem }) => (
  <div className="sidebar">
    <Container>
      <SiteTitle />
      <Nav vertical>
        <NavButton activeNavItem={activeNavItem} name="About Me" onClick={onClickNavItem} />
        <NavButton activeNavItem={activeNavItem} name="Experience" onClick={onClickNavItem} />
        <NavButton activeNavItem={activeNavItem} name="Projects" onClick={onClickNavItem} />
        <NavButton activeNavItem={activeNavItem} name="Recommendations" onClick={onClickNavItem} />
      </Nav>
    </Container>
  </div>
);

Sidebar.propTypes = {
  activeNavItem: PropTypes.string.isRequired,
  onClickNavItem: PropTypes.func.isRequired,
};

export default Sidebar;
