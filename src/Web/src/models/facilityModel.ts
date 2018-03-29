import { AddressModel } from './addressModel';

export class FacilityModel {
  readonly id: string;
  public firstName: string;
  public gpsLocation: string;
  public address: AddressModel;
}

export default FacilityModel;