import React from 'react';
import PropTypes from 'prop-types';

const PageContainer = ({ title, children }) => (
  <div>
    {title === 'About Me' ? <h1>Hi, I&apos;m Eric Stermer</h1> : <h1>{title}</h1>}
    <hr />
    {children}
  </div>
);

PageContainer.propTypes = {
  children: PropTypes.node.isRequired,
  title: PropTypes.string.isRequired,
};

export default PageContainer;
