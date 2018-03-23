import * as React from 'react';

interface DoctorItemProps {
  name: string;
  specializations: string;
  facilityAddress: string;
  photo: any;
}

class DoctorItem extends React.Component<DoctorItemProps, {}> {
  render() {
    return (
      <div>
        <section className='list'>
          <div className='photo'>
            <img src={this.props.photo} />
          </div>
          <div className='info'>
            <a href='#' className='name'>{this.props.name}</a>
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
      </div>
    );
  }
}

export default DoctorItem;