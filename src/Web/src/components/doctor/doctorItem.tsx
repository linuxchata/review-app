import * as React from "react";
import { Link } from "react-router-dom";

import DoctorModel from "../../models/DoctorModel";

import "../../styles/doctorItem.scss";

interface IDoctorItemProps {
  photo: any;
  doctor: DoctorModel;
}

class DoctorItem extends React.Component<IDoctorItemProps, {}> {
  render(): any {
    const address: any = this.props.doctor.facility.address;
    const formattedAddress: string = `${address.city}`;
    return (
      <section className="doctor-item-main">
        <div className="photo">
          <img src={this.props.photo} alt="photo" />
        </div>
        <div className="info">
          <Link to={`/doctor/${this.props.doctor.id}`} className="name">{this.props.doctor.name}</Link>
          <p className="spec">{this.props.doctor.specializations.join(", ")}</p>
          <div className="rating-wrapper">
            <div className="rating">
              <span>☆</span>
              <span>☆</span>
              <span>★</span>
              <span>★</span>
              <span>★</span>
            </div>
          </div>
          <p className="address">{formattedAddress}</p>
        </div>
      </section>
    );
  }
}

export default DoctorItem;