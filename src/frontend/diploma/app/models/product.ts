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
  rating: number;
}

export interface IProductShort {
  id: number;
  name: string;
  price: number;
  rating: number;
}

export interface ICreateProduct {
  name: string;
  price: number;
  availableCount: number;
  measurementType: number;
  sellOrganizationId: number;
  description: string;
}

export interface IUpdateProduct extends ICreateProduct {
  id: number;
}

export enum MeasurementType {
  Thing,
  Gram,
  Kg,
  Tones,
}
