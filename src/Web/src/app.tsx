import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from 'mobx-react';

import { DoctorModel } from './models/DoctorModel';
import { createStores } from './stores/createStores';
import Routes from './components/routes/Routes';

import 'normalize.css';
import './styles/colors.scss';
import './styles/const.scss';
import './styles/styles.scss';
import './styles/rating.scss';

const doctorStore = createStores([]);

ReactDOM.render(
  <Provider {...doctorStore}>
    <Routes />
  </Provider>,
  document.getElementById("root")
);