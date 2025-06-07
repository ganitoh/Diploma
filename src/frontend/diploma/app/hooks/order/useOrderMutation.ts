import { useMutation, useQueryClient } from "@tanstack/react-query";

import { loginUser, registration } from "@/app/http/user";

import { ORDER_QUERY_KEY } from "@/app/constans/query";
import { useMessage } from "../useMessage";
import { createOrganization } from "@/app/http/organization";
import { createOrder } from "@/app/http/order";

export const useOrderMutation = () => {
  const client = useQueryClient();
  const { Success, Error, Process } = useMessage(ORDER_QUERY_KEY);

  const onSuccess = () => {
    client.invalidateQueries({ queryKey: [ORDER_QUERY_KEY] }).then();
    Success();
  };

  const onMutate = () => {
    Process();
  };

  const onError = (e: any) => {
    Error(e, 7);
  };

  const createOrderMutation = useMutation({
    mutationFn: createOrder,
    onSuccess: () => onSuccess(),
    onMutate: () => onMutate(),
    onError: (e) => onError(e),
  });

  return {
    createOrderMutation,
  };
};
