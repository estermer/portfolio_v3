'use client';

import { useState, useMemo } from 'react';
import { Col, Row, Input } from 'reactstrap';
import ProjectCard from './ProjectCard';
import projects from '../data/projects';

export default function ProjectsPageClient() {
  const [selectedType, setSelectedType] = useState('');

  const handleSelectProject = (e) => {
    setSelectedType(e.target.value);
  };

  const projectList = useMemo(() => {
    if (selectedType === '') {
      return projects;
    }
    return projects.filter((project) => project.tech === selectedType);
  }, [selectedType]);

  return (
    <div>
      <div className="mb-3">
        <Input type="select" name="select" onChange={handleSelectProject} value={selectedType}>
          <option value="">-- Filter Projects --</option>
          <option value="jquery">jQuery</option>
          <option value="node">Node</option>
          <option value="rails">Ruby on Rails</option>
          <option value="learning">Learning</option>
          <option value="contributing">Contributing</option>
        </Input>
      </div>
      <Row>
        {projectList.map((project) => (
          <Col key={project.title} xs="12" sm="6" md="6" lg="4" xl="3">
            <ProjectCard project={project} />
          </Col>
        ))}
      </Row>
    </div>
  );
}

