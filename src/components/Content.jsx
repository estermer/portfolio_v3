import React from 'react';
import PropTypes from 'prop-types';
import { Container } from 'reactstrap';
import PageContainer from './PageContainer';
import AboutMePage from './AboutMePage';
import RecommendationsPage from './RecommendationsPage';
import ExperiencePage from './ExperiencePage';
import ProjectsPage from './ProjectsPage';

class Content extends React.Component {
  static propTypes = {
    activeNavItem: PropTypes.string.isRequired,
  };

  state = { selectedProject: '' };

  handleSelectProject = e => {
    const selectedProject = e.target.value;
    console.log(selectedProject);
    this.setState(() => ({ selectedProject }));
  };

  render() {
    const { activeNavItem } = this.props;
    const { selectedProject } = this.state;
    return (
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
            <PageContainer title={activeNavItem} handleSelectProject={this.handleSelectProject}>
              <ProjectsPage selectedType={selectedProject} />
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
  }
}

export default Content;
