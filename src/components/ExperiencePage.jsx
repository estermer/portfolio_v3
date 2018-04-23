import React from 'react';
import { Card, CardBody, CardTitle, CardText } from 'reactstrap';

const ExperiencePage = () => (
  <div>
    <Card>
      <CardBody>
        <CardTitle>Languages and Frameworks</CardTitle>
        <CardText>
          HTML, CSS, JavaScript, jQuery, React, Ruby, Rails, MEAN, SQL, Java, Bootstrap
        </CardText>
      </CardBody>
    </Card>

    <Card>
      <CardBody>
        <CardTitle>Management Deployment</CardTitle>
        <CardText>Git, GitHub, Command Line, Vagrant, NPM, Webpack, Maven, Gradle</CardText>
      </CardBody>
    </Card>

    <Card>
      <CardBody>
        <CardTitle>Methodologies</CardTitle>
        <CardText>
          Object Oriented Programming, MVC Pattern, Test-Driven Development, Responsive Design,
          Authentication, User Stories, ERDs, Wireframing, Agile Development/Scrum
        </CardText>
      </CardBody>
    </Card>
  </div>
);

export default ExperiencePage;
