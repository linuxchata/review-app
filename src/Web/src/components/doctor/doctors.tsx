import * as React from 'react';

import DoctorItem from './DoctorItem';

import * as doctor from '../../images/sample_doctor.png';

class Doctors extends React.Component<{}, {}>{
  render() {
    return (
      <div>
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
      </div>
    )
  }
}

export default Doctors;