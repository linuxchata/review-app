import { observable } from 'mobx';

export class DoctorModel {
  readonly id: string;
  public name: string;
  public specializations: string[];
  public facilityAddress: string;
  public certificateNumber: string;
}

export default DoctorModel;