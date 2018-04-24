import React from 'react';
import { Col, Row } from 'reactstrap';
import ProjectCard from './ProjectCard';
import projects from '../resources/projects';

const ProjectsPage = () => (
  <Row>
    {projects.map(project => (
      <Col key={project.title} xs="12" sm="6" md="6" lg="4" xl="3">
        <ProjectCard project={project} />
      </Col>
    ))}
  </Row>
);

export default ProjectsPage;
