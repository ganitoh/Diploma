import { useQuery } from "@tanstack/react-query";
import { GET_CHAT_BY_ORDER_ID } from "@/app/constans/query";
import { getOrderById } from "@/app/http/order";
import { getChatByOrderId } from "@/app/http/chat";

export const useGetChatByOrderIdQuery = (orderId: number) => {
  return useQuery({
    queryFn: () => getChatByOrderId(orderId),
    queryKey: [GET_CHAT_BY_ORDER_ID, orderId],
    retry: false,
  });
};
