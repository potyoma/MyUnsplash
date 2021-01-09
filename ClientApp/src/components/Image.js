import React, { Component } from 'react';

export default class Image extends Component {    
    render() {
        return (
            <div className="container">
                <img src={ this.props.link } alt={ this.props.label }/>
            </div>
        )
    }
}