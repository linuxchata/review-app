import * as React from 'react';
import { BrowserRouter as Router, Route, Link, Switch } from 'react-router-dom';

import Navbar from './Navbar';
import Doctors from '../doctor/Doctors';
import Doctor from '../doctor/Doctor';
import About from '../about/About';
import NoMatch from '../NoMatch';

class Routes extends React.Component<{}, {}>{
  render() {
    return (
      <div>
        <Router>
          <div>
            <Navbar />
            <Switch>
              <Route exact path="/" component={Doctor} />
              <Route exact path="/doctors" component={Doctors} />
              <Route path="/about" component={About} />
              <Route component={NoMatch} />
            </Switch>
          </div>
        </Router>
      </div>
    )
  }
}

export default Routes;