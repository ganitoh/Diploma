import { IMessage } from "./message";

export interface IChat {
  id: number;
  orderId: number;
  FirstUserId: string;
  SecondUserId: string;
  Messages: IMessage[];
}

export interface ICreateChat {
  SecondUserId: string;
  OrderIsd: number;
}
