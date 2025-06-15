import { IMessage } from "./message";

export interface IChat {
  id: number;
  orderId: number;
  FirstUserId: string;
  secondUserId: string;
  messages: IMessage[];
}

export interface ICreateChat {
  secondUserId: string;
  orderIsd: number;
}
