export interface IProduct {
  id: number;
  name: string;
  description: string;
  price: number;
  availableCount: number;
  totalSold: number;
  measurementType: MeasurementType;
  isStock: boolean;
  sellOrganizationId: number;
  sellOrganizationName: string;
}

export interface IProductShort {
  id: number;
  name: string;
  price: number;
}

export interface ICreateProduct {
  name: string;
  price: number;
  availableCount: number;
  measurementType: number;
  sellOrganizationId: number;
  description: string;
}

export enum MeasurementType {
  Thing,
  Gram,
  Kg,
  Tones,
}
