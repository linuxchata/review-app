import { observable, computed, action } from 'mobx';
import axios from 'axios';

import { DoctorModel } from '../models/DoctorModel';

export class DoctorStore {
  private serviceName: string;

  @observable public doctors: Array<DoctorModel>;

  @observable public loading: boolean;

  constructor(doctors: DoctorModel[]) {
    this.doctors = doctors;
    this.serviceName = 'https://reviewappweb.azurewebsites.net/api/subject';
  }

  @action
  async getAll() {
    try {
      this.loading = true;
      const response = await axios.get(
        this.serviceName
      );

      if (response.data) {
        this.doctors = [];

        let doctorModels: Array<DoctorModel> = JSON.parse(JSON.stringify(response.data));
        this.doctors = [ ...this.doctors, ...doctorModels];
      }
      
      this.loading = false;
    } catch (error) {
      console.error(error);
      this.loading = false;
    }
  }
}

export default DoctorStore;