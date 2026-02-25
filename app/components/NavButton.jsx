'use client';

import Link from 'next/link';
import { usePathname } from 'next/navigation';
import { NavItem } from 'reactstrap';

const icons = {
  'About Me': 'user',
  Experience: 'briefcase',
  Projects: 'laptop',
  Recommendations: 'pencil',
};

export default function NavButton({ name, href }) {
  const pathname = usePathname();
  
  // Determine if this nav item is active
  const isActive = 
    (href === '/' && pathname === '/') ||
    (href !== '/' && pathname.startsWith(href));

  return (
    <Link href={href} style={{ textDecoration: 'none', color: 'inherit' }}>
      <NavItem
        className={isActive ? 'active' : 'inactive'}
      >
        <i className={`fa fa-${icons[name]}`} />
        <span>{name}</span>
      </NavItem>
    </Link>
  );
}

