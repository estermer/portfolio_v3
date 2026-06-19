import Image from 'next/image';
import { Card, CardHeader, CardFooter, CardBody } from 'reactstrap';

const ProjectCard = ({ project }) => (
  <a target="_blank" rel="noopener noreferrer" href={project.url}>
    <Card className="project-card">
      <CardHeader className={`text-center bg-${project.tech} text-white`}>
        {project.title}
      </CardHeader>
      <div style={{ display: 'flex', justifyContent: 'center', marginTop: '1rem' }}>
        <Image
          className="project-img"
          src={`/images/projects/${project.key}.${project.ext || 'jpg'}`}
          alt={project.title}
          width={100}
          height={100}
          style={{ objectFit: 'contain' }}
        />
      </div>
      <CardBody />
      <CardFooter className="text-center bg-white text-dark">{project.description}</CardFooter>
    </Card>
  </a>
);

export default ProjectCard;

