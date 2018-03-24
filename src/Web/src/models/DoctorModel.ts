import { observable } from 'mobx';

export class DoctorModel {
  readonly id: number;
  @observable public name: string;
  @observable public specialization: string;
  @observable public facilityAddress: string;

  constructor(name: string, specialization: string, facilityAddress: string) {
    this.id = 1;
    this.name = name;
    this.specialization = specialization;
    this.facilityAddress = facilityAddress;
  }
}

export default DoctorModel;