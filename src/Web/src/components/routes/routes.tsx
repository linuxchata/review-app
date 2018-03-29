import * as React from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";

import Navbar from "./Navbar";
import Home from "../Home";
import Doctors from "../doctor/Doctors";
import Doctor from "../doctor/Doctor";
import About from "../about/About";
import NoMatch from "../NoMatch";

class Routes extends React.Component<{}, {}> {
  render(): any {
    return (
      <div>
        <Router>
          <div>
            <Navbar />
            <Switch>
              <Route exact path="/" component={Home} />
              <Route exact path="/doctors" component={Doctors} />
              <Route exact path="/doctor/:_id" component={Doctor} />
              <Route path="/about" component={About} />
              <Route component={NoMatch} />
            </Switch>
          </div>
        </Router>
      </div>
    );
  }
}

export default Routes;