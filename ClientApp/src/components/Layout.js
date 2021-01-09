import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

import UploadPopup from './UploadPopup';

export class Layout extends Component {
  static displayName = Layout.name;

  state = {
    seen: false
  };

  togglePop = () => {
    this.setState({
      seen: !this.state.seen
    });

    console.log("togglePop was pressed");
  };

  render () {
    return (
      <div>
        <NavMenu togglePop={this.togglePop}/>
        {this.state.seen ? <UploadPopup toggle={this.togglePop} /> : null}
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
