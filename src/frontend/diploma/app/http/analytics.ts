import { IAnalytics } from "../models/analytics";
import { IAnalyticsRequest, IResponse } from "../models/api";
import { organizaitonClient } from "./client";

export const getSellOrderAnalytics = async (
  params: IAnalyticsRequest
): Promise<IResponse<IAnalytics[]>> =>
  (await organizaitonClient.get("/Analytics/GetSellOrderAnalytics", { params }))
    .data;

export const getProductAnalytics = async (
  params: IAnalyticsRequest
): Promise<IResponse<IAnalytics[]>> =>
  (await organizaitonClient.get("/Analytics/GetProductAnalytics", { params }))
    .data;
