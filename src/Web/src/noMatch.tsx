import * as React from 'react';

import './styles/noMatch.scss';

interface NoMatchProps {
  location: any;
}

const NoMatch: React.StatelessComponent<NoMatchProps> = () => {
  return (
    <div className='no-match'>
      <p>Page not found</p>
      <p>No matches for {this.props.location.pathname} path</p>
    </div>
  )
}

export default NoMatch;