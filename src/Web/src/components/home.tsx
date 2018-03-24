import * as React from 'react';

import '../styles/home.scss';

export interface HomeProps {
}

export interface HomeState {
}

class Home extends React.Component<HomeProps, HomeState>{
  constructor(props: HomeProps, context: any) {
    super(props, context);
  }

  render() {
    return (
      <section className='main-container'>
        <header>
          <h2>Welcome!</h2>
        </header>
        <p>Review Application</p>
      </section>
    )
  }
}

export default Home;