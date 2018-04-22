import React from 'react';
import PropTypes from 'prop-types';
import { Container } from 'reactstrap';

const Content = ({ activeNavItem }) => (
  <div className="content">
    <Container>
      <p>I have content for {activeNavItem} page</p>
      {/* activeNaveItem === 'About Me' ? <AboutMePage /> : null */}
      {/* activeNaveItem === 'Experience' ? <ExperiencePage /> : null */}
      {/* activeNaveItem === 'Projects' ? <ProjectsPage /> : null */}
      {/* activeNaveItem === 'Recommendations' ? <RecommendationsPage /> : null */}
    </Container>
  </div>
);

Content.propTypes = {
  activeNavItem: PropTypes.string.isRequired,
};

export default Content;
