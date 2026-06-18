import React from 'react';
import PropTypes from 'prop-types';
import { Input } from 'reactstrap';

const PageContainer = ({ title, children, handleSelectProject }) => (
  <div>
    {title === 'Projects' ? (
      <span className="pull-right">
        <Input type="select" name="select" onChange={handleSelectProject}>
          <option value="">-- Filter Projects --</option>
          <option value="jquery">jQuery</option>
          <option value="node">Node</option>
          <option value="rails">Ruby on Rails</option>
          <option value="learning">Learning</option>
          <option value="contributing">Contributing</option>
        </Input>
      </span>
    ) : null}
    {title === 'About Me' ? <h1>Hi, I&apos;m Eric Stermer</h1> : <h1>{title}</h1>}
    <hr />
    {children}
  </div>
);

PageContainer.propTypes = {
  children: PropTypes.node.isRequired,
  handleSelectProject: PropTypes.func,
  title: PropTypes.string.isRequired,
};

PageContainer.defaultProps = {
  handleSelectProject: null,
};

export default PageContainer;
