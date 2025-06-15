import { useMutation, useQueryClient } from "@tanstack/react-query";

import { CHAT_QUERY_KEY } from "@/app/constans/query";
import { useMessage } from "../useMessage";
import { createChat } from "@/app/http/chat";

export const useChatMutation = () => {
  const client = useQueryClient();
  const { Success, Error, Process } = useMessage(CHAT_QUERY_KEY);

  const onSuccess = () => {
    client.invalidateQueries({ queryKey: [CHAT_QUERY_KEY] }).then();
    Success();
  };

  const onMutate = () => {
    Process();
  };

  const onError = (e: any) => {
    Error(e, 7);
  };

  const createChatMutation = useMutation({
    mutationFn: createChat,
    onMutate: () => onMutate(),
    onError: (e) => onError(e),
  });

  return {
    createChatMutation,
  };
};
