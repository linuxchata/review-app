import * as React from 'react';

import '../../styles/about.scss';

const About: React.StatelessComponent<{}> = () => {
  return (
    <section className='main-container'>
      <header>
        <h2>About</h2>
      </header>
      <p>Copyright (C) 2018 Pylyp Lebediev</p>
    </section>
  )
}

export default About;