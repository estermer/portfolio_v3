import { Card, CardBody, CardTitle, CardText } from 'reactstrap';

const ExperiencePage = () => (
  <div>
    <Card>
      <CardBody>
        <CardTitle tag="h5">Languages and Frameworks</CardTitle>
        <CardText>
          HTML, CSS, JavaScript(ES5 and ES6), jQuery, React, Ruby on Rails, NodeJS, AngularJS,
          ExpressJS, .NET Core, Entity Framework Core, MongoDB, SQL, Java, Bootstrap, Salesforce Lightning, SDFC Apex
        </CardText>
      </CardBody>
    </Card>

    <Card>
      <CardBody>
        <CardTitle tag="h5">Cloud and Infrastructure</CardTitle>
        <CardText>
          AWS, Azure, Docker, Kafka, RabbitMQ, Redis
        </CardText>
      </CardBody>
    </Card>

    <Card>
      <CardBody>
        <CardTitle tag="h5">Management Deployment</CardTitle>
        <CardText>Git, GitHub/GitLab, Command Line, Vagrant, NPM, Webpack, Maven, Gradle</CardText>
      </CardBody>
    </Card>

    <Card>
      <CardBody>
        <CardTitle tag="h5">Methodologies</CardTitle>
        <CardText>
          Object Oriented Programming, MVC Pattern, Test-Driven Development, Responsive Design,
          Authentication, User Stories, ERDs, Wireframing, Agile Development/Scrum
        </CardText>
      </CardBody>
    </Card>
  </div>
);

export default ExperiencePage;

