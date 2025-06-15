import { IResponse } from "../models/api";
import { IChat, ICreateChat } from "../models/chat";
import { chatClient } from "./client";

export const createChat = async (
  data: ICreateChat
): Promise<IResponse<number>> =>
  (await chatClient.post("/Chat/CreateChat", data)).data;

export const getChatByOrderId = async (
  orderId: number
): Promise<IResponse<IChat>> =>
  (
    await chatClient.get("/Chat/GetChat", {
      params: { orderId },
    })
  ).data;
