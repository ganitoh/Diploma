import { useQuery } from "@tanstack/react-query";
import {
  GET_PRODUCT_ANALYTICS,
  GET_SELL_ORDER_ANALYTICS,
  GET_ORDER_ANALYTICS_BY_STATUS,
} from "@/app/constans/query";
import {
  getProductAnalytics,
  getSellOrderAnalytics,
  GetSellOrderAnalyticsByStatus,
} from "@/app/http/analytics";
import {
  IAnalyticsOrderByStatusRequest,
  IAnalyticsRequest,
} from "@/app/models/api";

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

export const useGetSellOrderAnalyticsByStatusQuery = (
  params: IAnalyticsOrderByStatusRequest
) => {
  return useQuery({
    queryFn: () => GetSellOrderAnalyticsByStatus(params),
    queryKey: [GET_ORDER_ANALYTICS_BY_STATUS, params],
    retry: false,
  });
};
