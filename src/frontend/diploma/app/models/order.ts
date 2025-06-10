import { IProduct } from "./product";

export interface IOrder {
  id: number;
  totalPrice: number;
  deliveryDate: string;
  createDate: string;
  status: OrderStatus;
  statusText: string;
  sellerOrganizationId: number;
  buyerOrganizationId: number;
  sellerOrganizationName: string;
  buyerOrganizationName: string;
  productId: number;
  product: IProduct;
}

export interface ICreateOrder {
  sellerOrganizationId: number;
  productId: number;
  quantity: number;
}

export enum OrderStatus {
  Created = 1,
  Collected,
  InDelivery,
  Close,
}
