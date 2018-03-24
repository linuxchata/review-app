import * as React from 'react';
import { Link } from "react-router-dom";

import '../../styles/navbar.scss';

class Navbar extends React.Component<{}, {}>{
  render() {
    return (
      <div className='navbar'>
        <nav>
          <ul>
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/doctors">Doctors</Link>
            </li>
            <li>
              <Link to="/about">About</Link>
            </li>
          </ul>
        </nav>
      </div>
    )
  }
}

export default Navbar;