import * as React from 'react';

export interface IndexProps {
}

export interface IndexState {
}

class Index extends React.Component<IndexProps, IndexState>{
  constructor(props: IndexProps, context: any) {
    super(props, context);
  }

  render() {
    return <div>Welcome!</div>
  }
}

export default Index;