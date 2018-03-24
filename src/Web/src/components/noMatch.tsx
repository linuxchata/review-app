import * as React from 'react';

import '../styles/noMatch.scss';

interface NoMatchProps {
  location: any;
}

const NoMatch: React.StatelessComponent<NoMatchProps> = ({ location }) => {
  return (
    <div className='main-container'>
      <header>
        <h2>Page not found</h2>
      </header>
      <p>No matches for {location.pathname} path</p>
    </div>
  )
}

export default NoMatch;