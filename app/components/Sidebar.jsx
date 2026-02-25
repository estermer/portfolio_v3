'use client';

import { Container, Nav } from 'reactstrap';
import NavButton from './NavButton';
import SiteTitle from './SiteTitle';
import SocialIcons from './SocialIcons';

const Sidebar = () => (
  <div className="sidebar">
    <Container>
      <SiteTitle />
      <SocialIcons />
      <hr />
      <Nav vertical>
        <NavButton name="About Me" href="/" />
        <NavButton name="Experience" href="/experience" />
        <NavButton name="Projects" href="/projects" />
        <NavButton name="Recommendations" href="/recommendations" />
      </Nav>
      <hr />
    </Container>
  </div>
);

export default Sidebar;

