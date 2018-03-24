import * as React from 'react';
import { Link } from 'react-router-dom';

import '../../styles/doctorItem.scss';

interface DoctorItemProps {
  name: string;
  specializations: string;
  facilityAddress: string;
  photo: any;
}

class DoctorItem extends React.Component<DoctorItemProps, {}> {
  render() {
    return (
      <section className='doctor-item-main'>
        <div className='photo'>
          <img src={this.props.photo} alt='photo' />
        </div>
        <div className='info'>
          <Link to='/doctor' className='name'>{this.props.name}</Link>
          <p className='spec'>{this.props.specializations}</p>
          <div className='rating-wrapper'>
            <div className='rating'>
              <span>☆</span>
              <span>☆</span>
              <span>★</span>
              <span>★</span>
              <span>★</span>
            </div>
          </div>
          <p className='address'>{this.props.facilityAddress}</p>
        </div>
      </section>
    );
  }
}

export default DoctorItem;