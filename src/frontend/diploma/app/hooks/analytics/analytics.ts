import { useQuery } from "@tanstack/react-query";
import {
  GET_PRODUCT_ANALYTICS,
  GET_SELL_ORDER_ANALYTICS,
} from "@/app/constans/query";
import {
  getProductAnalytics,
  getSellOrderAnalytics,
} from "@/app/http/analytics";
import { IAnalyticsRequest } from "@/app/models/api";

export const useGetSellOrderAnalyticsQuery = (params: IAnalyticsRequest) => {
  return useQuery({
    queryFn: () => getSellOrderAnalytics(params),
    queryKey: [GET_SELL_ORDER_ANALYTICS, params],
    retry: false,
  });
};

export const useGetProductAnalyticsQuery = (params: IAnalyticsRequest) => {
  return useQuery({
    queryFn: () => getProductAnalytics(params),
    queryKey: [GET_PRODUCT_ANALYTICS, params],
    retry: false,
  });
};
