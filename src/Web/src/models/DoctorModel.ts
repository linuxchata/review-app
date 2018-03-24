import { observable } from 'mobx';

export class DoctorModel {
  readonly id: number;
  @observable public name: string;
  @observable public specialization: string;
  @observable public facilityAddress: string;

  constructor(id: number, name: string, specialization: string, facilityAddress: string) {
    this.id = id;
    this.name = name;
    this.specialization = specialization;
    this.facilityAddress = facilityAddress;
  }
}

export default DoctorModel;