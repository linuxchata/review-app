import * as React from "react";
import * as ReactDOM from "react-dom";
import Hello from "./Hello";

import './styles/styles.scss';
import * as logo from './images/home.png';
import * as vr46 from './images/vr46.png';

ReactDOM.render(
    <div><img src={logo} /> <img src={vr46} /> <Hello name="Willson" /></div>,
    document.getElementById("root")
);