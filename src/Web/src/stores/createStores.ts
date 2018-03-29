import { DoctorModel } from "../models/DoctorModel";
import { DoctorStore } from "./DoctorStore";
import { STORE_DOCTOR } from "../constants/Stores";

export function createStores(doctors: DoctorModel[]): any {
  const doctorStore: DoctorStore = new DoctorStore(doctors);

  return { [STORE_DOCTOR]: doctorStore };
}