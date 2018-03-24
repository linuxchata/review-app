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

    return (
      <div>
        {doctorStore.doctors.length > 0 ? doctorStore.doctors.map((doctor) => (
          <DoctorItem
            key={doctor.id}
            name={doctor.name}
            specializations={doctor.specialization}
            facilityAddress={doctor.facilityAddress}
            photo={doctorPhoto} />
        )) : <section className='main-container'><p>No data</p></section>}
      </div>
    );
  }
}

export default Doctors;