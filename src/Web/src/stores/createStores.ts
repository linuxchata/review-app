import { DoctorModel } from '../models/DoctorModel';
import { DoctorStore } from './DoctorStore';
import { STORE_DOCTOR } from '../constants/Stores';

export function createStores(doctors: DoctorModel[]) {
  const doctorStore = new DoctorStore(doctors);

  return { [STORE_DOCTOR]: doctorStore }
}