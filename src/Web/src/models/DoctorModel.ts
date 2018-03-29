import {FacilityModel} from './facilityModel';

export class DoctorModel {
  readonly id: string;
  public firstName: string;
  public middleName: string;
  public lastName: string;
  public name: string;
  public generalRating: number;
  public certificateNumber: string;
  public schools: string[];
  public internships: string[];
  public degrees: string[];
  public specializations: string[];
  public publications: string[];
  public diseases: string[];
  public languages: string[];
  public facility: FacilityModel;
}

export default DoctorModel;