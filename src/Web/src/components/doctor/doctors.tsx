import * as React from 'react';
import { inject, observer } from 'mobx-react';

import { DoctorStore } from '../../stores/DoctorStore';
import { STORE_DOCTOR } from '../../constants/Stores';

import DoctorItem from './DoctorItem';
import DoctorModel from '../../models/DoctorModel';

import * as doctorPhoto from '../../images/sample_doctor.png';

interface DoctorsProps {
  [STORE_DOCTOR]: DoctorStore;
}

export interface DoctorsState { }

@inject(STORE_DOCTOR)
@observer
class Doctors extends React.Component<DoctorsProps, DoctorsState>{
  constructor(props?: DoctorsProps, context?: any) {
    super(props, context);
  }

  componentDidMount() {
    this.props.doctorStore.getAll();
  }

  render() {
    const doctorStore = this.props[STORE_DOCTOR] as DoctorStore;

    let container = null;

    if (doctorStore.loading) {
      container = <section className='loading-container'><p>Loading data...</p></section>
    }
    else {
      if (doctorStore.doctors.length > 0) {
        container = doctorStore.doctors.map((doctor) => (
          <DoctorItem
            key={doctor.id}
            name={doctor.name}
            specializations={doctor.specialization}
            facilityAddress={doctor.facilityAddress}
            photo={doctorPhoto} />
        ))
      }
      else {
        container = <section className='main-container'><p>No data</p></section>
      }
    }

    return (
      <div>{container}</div>
    );
  }
}

export default Doctors;