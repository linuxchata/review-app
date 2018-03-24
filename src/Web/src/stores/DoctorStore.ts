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

      if (!response.data) {
        this.doctors.push(new DoctorModel(
          this.id++,
          "mgr Adam Kondrad Lewanowicz",
          "Psycholog, Terapeuta, Psychoterapeuta",
          "ul.Grzegórzecka 67H klatka B /41(Wiślane Tarasy) I piętro"
        ));
      }

    } catch (error) {
      console.error(error);
    }
  }
}

export default DoctorStore;