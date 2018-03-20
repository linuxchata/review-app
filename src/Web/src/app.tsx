import * as React from "react";
import * as ReactDOM from "react-dom";
import Hello from "./Hello";

import './styles/styles.scss';

ReactDOM.render(
  <div><Hello name="Oliver" /></div>,
  document.getElementById("root")
);