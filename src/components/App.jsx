import React from 'react';
import Sidebar from './Sidebar';
import Content from './Content';

class App extends React.Component {
  state = {
    activeNavItem: 'About Me',
  };

  onClickNavItem = navItem => {
    this.setState(() => ({ activeNavItem: navItem }));
  };

  render() {
    const { activeNavItem } = this.state;

    return (
      <div>
        <Sidebar activeNavItem={activeNavItem} onClickNavItem={this.onClickNavItem} />
        <Content activeNavItem={activeNavItem} />
      </div>
    );
  }
}

export default App;
