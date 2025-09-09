export interface IMessage {
  Id: number;
  userId: string;
  createdDatetime: Date;
  text: string;
  ChatId: number;
}

export interface ICreateMessage {
  chatId: number;
  orderId: number;
  Text: string;
  userId: string;
}
