import * as React from "react";

import "../styles/home.scss";

export interface IHomeProps {
}

export interface IHomeState {
}

class Home extends React.Component<IHomeProps, IHomeState> {
  constructor(props: IHomeProps, context: any) {
    super(props, context);
  }

  render() {
    return (
      <section className="main-container">
        <header>
          <h2>Welcome!</h2>
        </header>
        <p>Review Application</p>
      </section>
    );
  }
}

export default Home;