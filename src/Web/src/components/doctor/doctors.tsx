import * as React from "react";
import { inject, observer } from "mobx-react";

import { DoctorStore } from "../../stores/DoctorStore";
import { STORE_DOCTOR } from "../../constants/Stores";

import DoctorItem from "./DoctorItem";
import DoctorModel from "../../models/DoctorModel";

import * as doctorPhoto from "../../images/sample_doctor.png";

interface IDoctorsProps {
  [STORE_DOCTOR]: DoctorStore;
}

export interface IDoctorsState {
}

@inject(STORE_DOCTOR)
@observer
class Doctors extends React.Component<IDoctorsProps, IDoctorsState> {
  constructor(props?: IDoctorsProps, context?: any) {
    super(props, context);
  }

  componentDidMount(): void {
    this.props.doctorStore.getAll();
  }

  render(): any {
    const doctorStore: DoctorStore = this.props[STORE_DOCTOR] as DoctorStore;

    let container: any = null;
    if (doctorStore.loading) {
      container = (<section className="loading-container"><p>Loading data...</p></section>);
    } else {
      if (doctorStore.doctors.length > 0) {
        container = (doctorStore.doctors.map((item) => (
          <DoctorItem key={item.id} photo={doctorPhoto} doctor={item} />
        )));
      } else {
        container = (<section className="main-container"><p>No data</p></section>);
      }
    }

    return <div>{container}</div>;
  }
}

export default Doctors;