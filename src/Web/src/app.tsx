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

const defaultDoctors = [
  new DoctorModel(
    "mgr Adam Kondrad Lewanowicz",
    "Psycholog, Terapeuta, Psychoterapeuta",
    "ul.Grzegórzecka 67H klatka B /41(Wiślane Tarasy) I piętro"),
  new DoctorModel(
    "lek. dent. Marek Mastalerz",
    "Stomatolog",
    "Kurniki 4, Kraków Estetica Beauty Dent"),
  new DoctorModel(
    "lek. dent. Marek Mastalerz",
    "Stomatolog",
    "Kurniki 4, Kraków Estetica Beauty Dent")
];

const doctorStore = createStores(defaultDoctors);

ReactDOM.render(
  <Provider {...doctorStore}>
    <Routes />
  </Provider>,
  document.getElementById("root")
);