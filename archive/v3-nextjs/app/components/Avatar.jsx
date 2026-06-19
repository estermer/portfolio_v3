import Image from 'next/image';
import Link from 'next/link';

const Avatar = () => (
  <Link href="/">
    <Image
      className="avatar"
      src="/images/avatar.jpg"
      alt="Eric Stermer Avatar"
      width={120}
      height={120}
    />
  </Link>
);

export default Avatar;

