import React, { Component } from 'react';
import Image from './Image';

export class Home extends Component {
  static displayName = Home.name;
  static apiLink = document.URL + "/api";

  state = {
    images: []
  }

  render () {
    fetch(this.apiLink + "/getimages")
      .then(r => this.setState({ images: r.json() }));

    return (
      <div>
        { 
          this.state.images.length > 0 
          ? this.state.images.forEach(i => <Image link={i.link} />) 
          : <h3>Sorry, no images available...</h3>
        }
      </div>
    );
  }
}
