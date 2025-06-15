import { IDownloadDocument, IResponse } from "../models/api";
import { ICreateOrder, IOrder } from "../models/order";
import { organizaitonClient } from "./client";

export const getOrderById = async (
  organizationId: number
): Promise<IResponse<IOrder>> =>
  (
    await organizaitonClient.get("/Order/GetOrderById", {
      params: { organizationId },
    })
  ).data;

export const getInvoiceForOrder = async (
  orderId: number
): Promise<IResponse<IDownloadDocument>> =>
  (
    await organizaitonClient.get("/Order/getInvoiceForOrder", {
      params: { orderId },
    })
  ).data;

export const createOrder = async (
  data: ICreateOrder
): Promise<IResponse<number>> =>
  (await organizaitonClient.post("/Order/CreateOrder", data)).data;
