import * as React from 'react';

class Navbar extends React.Component<{}, {}>{
  render() {
    return (
      <div>
        <nav>
          <ul>
            <li>
              <a href="#">Home</a>
            </li>
            <li>
              <a className="active" href="#">News</a>
            </li>
            <li>
              <a href="#">Contact</a>
            </li>
            <li>
              <li>
                <a href="#">About</a>
              </li>
            </li>
          </ul>
        </nav>
      </div>)
  }
}

export default Navbar;