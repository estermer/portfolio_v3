import React from 'react';
import { Card, CardBody, CardTitle, CardText } from 'reactstrap';

const AboutMePage = () => (
  <div>
    <Card>
      <CardBody>
        <CardTitle>My Mission</CardTitle>
        <CardText>
          I strive to find solutions that drive incremental change in simple everyday experiences.
        </CardText>
      </CardBody>
    </Card>

    <Card>
      <CardBody>
        <CardTitle>My Coding Story</CardTitle>
        <CardText>
          My interest in coding began when I was prompted to watch &quot;Coding Stars&quot; and
          complete &quot;The Hour of Code&quot; on Khan Academy. From that moment I knew I had found
          my passion.
        </CardText>
      </CardBody>
    </Card>

    <Card>
      <CardBody>
        <CardTitle>A Little More About Me</CardTitle>
        <CardText>
          <p>
            I was born and raised in Binghamton, NY. I loved playing sports growing up, baseball,
            football, and basketball. But around middle school I started running cross country and
            track. I ran competitively all the way through to college where I had the honor of
            winning a few National Championships.
          </p>
          <p>
            After College I lived in Lynchburg, Virginia for sometime after getting married to my
            wife and best friend.
          </p>
          <p>
            Now I live in Provdence, RI where I have become quite the foodie, trying the vast
            choices of great eateries in the area.
          </p>
        </CardText>
      </CardBody>
    </Card>
    {/* <a style={{ color: 'black' }} href="https://www.youtube.com/watch?v=dU1xS07N-FA">
      Coding Stars
    </a> */}
    {/* <a style={{ color: 'black' }} href="https://www.khanacademy.org/hourofcode">
      The Hour of Code
    </a> */}
  </div>
);

export default AboutMePage;
