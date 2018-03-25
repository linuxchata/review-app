import { observable } from 'mobx';

export class DoctorModel {
  readonly id: number;
  public name: string;
  public specialization: string;
  public facilityAddress: string;
  public certificateNumber: string;
}

export default DoctorModel;