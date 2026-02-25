import './globals.css';
import Sidebar from './components/Sidebar';

export const metadata = {
  title: 'Eric Stermer',
  description: 'Eric Stermer - Web Developer Portfolio',
};

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body>
        <Sidebar />
        <div className="content">
          {children}
        </div>
      </body>
    </html>
  );
}

