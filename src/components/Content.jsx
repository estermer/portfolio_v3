import React from 'react';
import PropTypes from 'prop-types';
import { Container } from 'reactstrap';
import PageContainer from './PageContainer';
import AboutMePage from './AboutMePage';
import RecommendationsPage from './RecommendationsPage';
import ExperiencePage from './ExperiencePage';

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
          <ExperiencePage />
        </PageContainer>
      ) : null}
      {activeNavItem === 'Projects' ? (
        <PageContainer title={activeNavItem}>
          <p>I have content for {activeNavItem} page</p>
        </PageContainer>
      ) : null}
      {activeNavItem === 'Recommendations' ? (
        <PageContainer title={activeNavItem}>
          <RecommendationsPage />
        </PageContainer>
      ) : null}
    </Container>
  </div>
);

Content.propTypes = {
  activeNavItem: PropTypes.string.isRequired,
};

export default Content;
