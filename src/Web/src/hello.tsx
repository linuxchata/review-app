import * as React from "react";

interface HelloProps {
  name: string;
}

class Hello extends React.Component<HelloProps, {}> {
  render() {
    return <div><h1>Test</h1><div>Hello, {this.props.name}</div></div>;
  }
}

export default Hello;