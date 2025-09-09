import { useQuery } from "@tanstack/react-query";
import {
  GET_ORDER_BY_ID,
  GET_PAGED_ORDER_BY_USER_ID,
} from "@/app/constans/query";
import { getOrderById, getPagedOrderByUserId } from "@/app/http/order";
import { IGetPagedOrderByUserId } from "@/app/models/order";

export const useGetOrderByIdQuery = (organizationId: number) => {
  return useQuery({
    queryFn: () => getOrderById(organizationId),
    queryKey: [GET_ORDER_BY_ID, organizationId],
    retry: false,
  });
};

export const useGetPagedOrderByUserIdQuery = (
  params: IGetPagedOrderByUserId
) => {
  return useQuery({
    queryFn: () => getPagedOrderByUserId(params),
    queryKey: [GET_PAGED_ORDER_BY_USER_ID, params],
    retry: false,
  });
};
