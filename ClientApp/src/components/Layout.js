import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  state = {
    seen: false
  };

  togglePop = () => {
    this.setState({
      seen: !this.state.seen
    });
  };

  render () {
    return (
      <div>
        <NavMenu />
        <Container>
          {this.props.children}
          {this.state.seen ? <UploadPopup toggle={this.togglePop} /> : null}
        </Container>
      </div>
    );
  }
}
