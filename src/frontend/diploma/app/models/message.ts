export interface IMessage {
  Id: number;
  userId: string;
  CreatedDatetime: Date;
  Text: string;
  ChatId: number;
}

export interface ICreateMessage {
  chatId: number;
  orderId: number;
  Text: string;
}
