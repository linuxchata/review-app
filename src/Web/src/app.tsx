import * as React from "react";
import * as ReactDOM from "react-dom";
import Navbar from './Navbar';
import DoctorItem from "./DoctorItem";

import 'normalize.css';
import './styles/colors.scss';
import './styles/const.scss';
import './styles/styles.scss';
import './styles/navbar.scss';
import './styles/rating.scss';
import './styles/doctors.scss';
import './styles/doctor.scss';

import * as doctor from './images/sample_doctor.png';

ReactDOM.render(
  <div>
    <Navbar />
    <DoctorItem
      name='mgr Adam Kondrad Lewanowicz'
      specializations='Psycholog, Terapeuta, Psychoterapeuta'
      facilityAddress='ul.Grzegórzecka 67H klatka B /41(Wiślane Tarasy) I piętro'
      photo={doctor}></DoctorItem>
    <DoctorItem
      name='mgr Adam Kondrad Lewanowicz'
      specializations='Psycholog, Terapeuta, Psychoterapeuta'
      facilityAddress='ul.Grzegórzecka 67H klatka B /41(Wiślane Tarasy) I piętro'
      photo={doctor}></DoctorItem>
  </div>,
  document.getElementById("root")
);