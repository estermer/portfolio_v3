import React from 'react';
import PropTypes from 'prop-types';
import { Col, Row } from 'reactstrap';
import ProjectCard from './ProjectCard';
import projects from '../resources/projects';

class ProjectsPage extends React.Component {
  static propTypes = {
    selectedType: PropTypes.string,
  };

  static defaultProps = {
    selectedType: '',
  };

  componentWillMount = () => {
    this.setState({ projectList: projects });
  };

  componentWillReceiveProps = nextProps => {
    const { selectedType } = nextProps;
    if (selectedType === '') {
      this.setState({ projectList: projects });
    } else {
      const projectList = projects.filter(this.filterProjects.bind(this, selectedType));
      this.setState({ projectList });
    }
  };

  filterProjects = (selectedType, project) => project.tech === selectedType;

  renderProjects = project => (
    <Col key={project.title} xs="12" sm="6" md="6" lg="4" xl="3">
      <ProjectCard project={project} />
    </Col>
  );

  render() {
    const { projectList } = this.state;
    return <Row>{projectList.map(this.renderProjects)}</Row>;
  }
}

export default ProjectsPage;
