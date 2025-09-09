import { IOrder } from "./order";
import { IProduct } from "./product";
import { IRequestParams } from "./api";

export interface IOrganiaiton {
  id: number;
  name: string;
  description: string;
  inn: string;
  email: string;
  legalAddress: string;
  isApproval: boolean;
  userId: string;
  products: IProduct[];
  sellOrders: IOrder[];
  buyOrders: IOrder[];
}

export interface IShortOrganiaiton {
  name: string;
  ratingValue: number;
  id: number;
}

export interface ICreateOrganiaiton {
  name: string;
  description: string;
  inn: string;
  email: string;
  legalAddress: string;
  isExternal?: boolean;
}

export interface IGetPagedOrganization extends IRequestParams {
  isExternal?: boolean;
}
