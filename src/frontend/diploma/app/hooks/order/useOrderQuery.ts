import { useQuery } from "@tanstack/react-query";
import { GET_ORDER_BY_ID } from "@/app/constans/query";
import { getOrderById } from "@/app/http/order";

export const useGetOrderByIdQuery = (organizationId: number) => {
  return useQuery({
    queryFn: () => getOrderById(organizationId),
    queryKey: [GET_ORDER_BY_ID, organizationId],
    retry: false,
  });
};
