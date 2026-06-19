import { Container } from 'reactstrap';
import AboutMePage from './components/AboutMePage';

export default function Home() {
  return (
    <Container>
      <h1>Hi, I&apos;m Eric Stermer</h1>
      <hr />
      <AboutMePage />
    </Container>
  );
}

