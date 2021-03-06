import React from 'react';
import { Card, CardBody, CardText } from 'reactstrap';

const RecomendationsPage = () => (
  <div>
    <Card>
      <CardBody>
        <CardText>
          <i>
            &ldquo;We hired Eric right out of General Assembly. I knew in our first conversation
            that he was smart and could get things done! This proved true throughout his time at
            Datarista. He initially started first on the front end, working with React and Redux,
            then moved into a full stack role where he worked on parts of our back end, as well. I
            highly recommend Eric for a software engineering position.&rdquo;
          </i>
        </CardText>
        <CardText>
          -{' '}
          <a
            href="https://www.linkedin.com/in/davecostantino/"
            rel="noopener noreferrer"
            target="_blank"
          >
            Dave Costantino
          </a>, <i>CTO, Datarista Inc.</i>
        </CardText>
      </CardBody>
    </Card>

    <Card>
      <CardBody>
        <CardText>
          <i>
            &ldquo;We hired Eric for his first professional developer job coming out of the General
            Assembly Developer Immersive. From the start he was clearly a very smart guy and we felt
            that with time he would learn the business aspects of being a developer. He quickly
            adapted his problem solving and critical thinking and we were proud to promote him from
            Jr. Developer to Full Stack Engineer, a title he rightly earned. He is personable,
            optimistic and hard working and delivers results. I would highly recommend Eric as a
            developer.&rdquo;
          </i>
        </CardText>
        <CardText>
          -{' '}
          <a
            href="https://www.linkedin.com/in/patsabatino/"
            rel="noopener noreferrer"
            target="_blank"
          >
            Pat Sabatino
          </a>, <i>CEO, Datarista Inc.</i>
        </CardText>
      </CardBody>
    </Card>

    <Card>
      <CardBody>
        <CardText>
          <i>
            &ldquo;Eric is an amazing instructor! Dedicated, prepared and patient. He taught me a
            ton in a short time. He has a great teaching style and I will be forever grateful for
            his contributions to my education.&rdquo;
          </i>
        </CardText>
        <CardText>
          -{' '}
          <a
            href="https://www.linkedin.com/in/patrick-mccarty/"
            rel="noopener noreferrer"
            target="_blank"
          >
            Patrick McCarty
          </a>, <i>Student, GA Web Development Immersive</i>
        </CardText>
      </CardBody>
    </Card>
  </div>
);

export default RecomendationsPage;
