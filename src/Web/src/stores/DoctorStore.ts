import { observable, computed, action } from 'mobx';
import axios from 'axios';

import { DoctorModel } from '../models/DoctorModel';

export class DoctorStore {
  constructor(doctors: DoctorModel[]) {
    this.doctors = doctors;
    this.id = 0;
  }

  private id: number;
  @observable public doctors: Array<DoctorModel>;

  @action
  async getAll() {
    try {
      const response = await axios.get(
        'https://reviewappweb.azurewebsites.net/api/subject'
      );
      console.log(response);

      if (response.data) {
        this.doctors = [];
        response.data.map((doctor: any) => {
          this.doctors.push(new DoctorModel(
            doctor.id,
            doctor.name,
            doctor.specializations.join(', '),
            doctor.facility.name
          ));
        });
      }

    } catch (error) {
      console.error(error);
    }
  }
}

export default DoctorStore;