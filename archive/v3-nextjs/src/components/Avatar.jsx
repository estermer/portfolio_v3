import React from 'react';
import avatar from '../resources/avatar.jpg';

const Avatar = () => (
  <a href="/#">
    <img className="avatar" src={avatar} alt="Eric Stermer Avatar" />
  </a>
);

export default Avatar;
