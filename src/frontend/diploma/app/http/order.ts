import { IDownloadDocument, IPagingResponse, IResponse } from "../models/api";
import {
  IChangeOrderStatus,
  ICreateOrder,
  IGetPagedOrderByUserId,
  IOrder,
} from "../models/order";
import { organizaitonClient } from "./client";

export const getOrderById = async (
  organizationId: number
): Promise<IResponse<IOrder>> =>
  (
    await organizaitonClient.get("/Order/GetOrderById", {
      params: { organizationId },
    })
  ).data;

export const getPagedOrderByUserId = async (
  params: IGetPagedOrderByUserId
): Promise<IResponse<IPagingResponse<IOrder>>> =>
  (
    await organizaitonClient.get("/Order/GetPagedOrderByUserId", {
      params: {
        isSellOrders: params.isSellOrders,
        pageNumber: params.pageNumber,
        pageSize: params.pageSize,
        status: params.status,
      },
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

export const ChangeOrderStatus = async (
  data: IChangeOrderStatus
): Promise<IResponse<number>> =>
  (await organizaitonClient.put("/Order/ChangeOrderStatus", data)).data;
