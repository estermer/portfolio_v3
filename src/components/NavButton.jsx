import React from 'react';
import PropTypes from 'prop-types';
import { NavItem } from 'reactstrap';

const icons = {
  'About Me': 'user',
  Experience: 'briefcase',
  Projects: 'laptop',
  Recommendations: 'pencil',
};

export default class NavButton extends React.Component {
  static propTypes = {
    activeNavItem: PropTypes.string.isRequired,
    name: PropTypes.string.isRequired,
    onClick: PropTypes.func.isRequired,
  };

  onClick = e => {
    const { onClick, name } = this.props;
    e && e.preventDefault();
    onClick(name);
  };

  render() {
    const { activeNavItem, name } = this.props;
    return (
      <NavItem active={activeNavItem === name} onClick={this.onClick}>
        <i className={`fa fa-${icons[name]}`} />
        <span>{name}</span>
      </NavItem>
    );
  }
}
