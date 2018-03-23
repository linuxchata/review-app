import * as React from 'react';
import { BrowserRouter as Router, Route, Link, Switch } from 'react-router-dom';

import Navbar from './Navbar';
import Doctors from './Doctors';
import Doctor from './Doctor';
import About from './About';
import NoMatch from './NoMatch';

class Routes extends React.Component<{}, {}>{
  render() {
    return (
      <div>
        <Router>
          <div>
            <Navbar />
            <Switch>
              <Route exact path="/" component={Doctor} />
              <Route exact path="/doctors2" component={Doctors} />
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