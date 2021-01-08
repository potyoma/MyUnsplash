import React, { Component } from 'react';

export class Image extends Component {
    static image = props.link;
    
    render() {
        return (
            <div className="container">
                <img src={this.image} />
            </div>
        )
    }
}