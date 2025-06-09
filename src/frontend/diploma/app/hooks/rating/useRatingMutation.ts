import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useMessage } from "../useMessage";
import { RATING_QUERY_KEY } from "@/app/constans/query";
import { createProduct } from "@/app/http/products";
import { createRating } from "@/app/http/rating";

export const useRatingMutation = () => {
  const client = useQueryClient();
  const { Success, Error, Process } = useMessage(RATING_QUERY_KEY);

  const onSuccess = () => {
    client.invalidateQueries({ queryKey: [RATING_QUERY_KEY] }).then();
    Success();
  };

  const onMutate = () => {
    Process();
  };

  const onError = (e: any) => {
    Error(e, 7);
  };

  const createRatingMutation = useMutation({
    mutationFn: createRating,
    onSuccess: () => onSuccess(),
    onMutate: () => onMutate(),
    onError: (e) => onError(e),
  });

  return {
    createRatingMutation,
  };
};
