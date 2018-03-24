import { observable, computed, action } from 'mobx';
import axios from 'axios';

import { DoctorModel } from '../models/DoctorModel';

export class DoctorStore {
  constructor(doctors: DoctorModel[]) {
    this.doctors = doctors;
  }

  @observable public doctors: Array<DoctorModel>;

  @action
  async getAll() {
    try {
      const response = await axios.get(
        'https://reviewappweb.azurewebsites.net/api/location'
      );
      console.log(response);
    } catch (error) {
      console.error(error);
    }
  }
}

export default DoctorStore;