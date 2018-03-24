import { observable, computed, action } from 'mobx';
import { DoctorModel } from '../models/DoctorModel';

export class DoctorStore {
  constructor(doctors: DoctorModel[]) {
    this.doctors = doctors;
  }

  @observable public doctors: Array<DoctorModel>;
}

export default DoctorStore;