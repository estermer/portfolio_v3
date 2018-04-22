import React from 'react';
import PropTypes from 'prop-types';
import { Container } from 'reactstrap';
import PageContainer from './PageContainer';
import AboutMePage from './AboutMePage';

const Content = ({ activeNavItem }) => (
  <div className="content">
    <Container>
      {activeNavItem === 'About Me' ? (
        <PageContainer title={activeNavItem}>
          <AboutMePage />
        </PageContainer>
      ) : null}
      {activeNavItem === 'Experience' ? (
        <PageContainer title={activeNavItem}>
          <p>I have content for {activeNavItem} page</p>
        </PageContainer>
      ) : null}
      {activeNavItem === 'Projects' ? (
        <PageContainer title={activeNavItem}>
          <p>I have content for {activeNavItem} page</p>
        </PageContainer>
      ) : null}
      {activeNavItem === 'Recommendations' ? (
        <PageContainer title={activeNavItem}>
          <p>I have content for {activeNavItem} page</p>
        </PageContainer>
      ) : null}
    </Container>
  </div>
);

Content.propTypes = {
  activeNavItem: PropTypes.string.isRequired,
};

export default Content;
