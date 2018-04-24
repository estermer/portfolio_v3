import React from 'react';
import PropTypes from 'prop-types';
import { Card, CardHeader, CardImg, CardFooter, CardBody } from 'reactstrap';

import images from '../resources/images';

const ProjectCard = ({ project }) => (
  <a target="_blank" rel="noopener noreferrer" href={project.url}>
    <Card className="project-card">
      <CardHeader className={`text-center bg-${project.tech} text-white`}>
        {project.title}
      </CardHeader>
      <CardImg className="project-img" src={images[project.key]} />
      <CardBody />
      <CardFooter className="text-center bg-white text-dark">{project.description}</CardFooter>
    </Card>
  </a>
);

ProjectCard.propTypes = {
  project: PropTypes.object.isRequired,
};

export default ProjectCard;
