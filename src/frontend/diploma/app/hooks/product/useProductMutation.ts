import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useMessage } from "../useMessage";
import { PRODUCT_QUERY_KEY } from "@/app/constans/query";
import { createProduct, updateProduct } from "@/app/http/products";

export const useProductMutation = () => {
  const client = useQueryClient();
  const { Success, Error, Process } = useMessage(PRODUCT_QUERY_KEY);

  const onSuccess = () => {
    client.invalidateQueries({ queryKey: [PRODUCT_QUERY_KEY] }).then();
    Success();
  };

  const onMutate = () => {
    Process();
  };

  const onError = (e: any) => {
    Error(e, 7);
  };

  const createProductMutation = useMutation({
    mutationFn: createProduct,
    onSuccess: () => onSuccess(),
    onMutate: () => onMutate(),
    onError: (e) => onError(e),
  });

  const updateProductMutation = useMutation({
    mutationFn: updateProduct,
    onSuccess: () => onSuccess(),
    onMutate: () => onMutate(),
    onError: (e) => onError(e),
  });

  return {
    createProductMutation,
    updateProductMutation,
  };
};
