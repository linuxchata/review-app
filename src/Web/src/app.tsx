import * as React from "react";
import * as ReactDOM from "react-dom";

import Routes from './components/routes/Routes';

import 'normalize.css';
import './styles/colors.scss';
import './styles/const.scss';
import './styles/styles.scss';
import './styles/rating.scss';

ReactDOM.render(
  <Routes />,
  document.getElementById("root")
);