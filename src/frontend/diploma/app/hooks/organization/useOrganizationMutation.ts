import { useMutation, useQueryClient } from "@tanstack/react-query";

import { ORGANIZATION_QUERY_KEY } from "@/app/constans/query";
import { useMessage } from "../useMessage";
import {
  createOrganization,
  verificationOrganization,
} from "@/app/http/organization";

export const useOrganizationMutation = () => {
  const client = useQueryClient();
  const { Success, Error, Process } = useMessage(ORGANIZATION_QUERY_KEY);

  const onSuccess = () => {
    client.invalidateQueries({ queryKey: [ORGANIZATION_QUERY_KEY] }).then();
    Success();
  };

  const onMutate = () => {
    Process();
  };

  const onError = (e: any) => {
    Error(e, 7);
  };

  const createOrganizationMutation = useMutation({
    mutationFn: createOrganization,
    onSuccess: () => onSuccess(),
    onMutate: () => onMutate(),
    onError: (e) => onError(e),
  });

  const verifyOrganizationMutation = useMutation({
    mutationFn: verificationOrganization,
    onSuccess: () => onSuccess(),
    onMutate: () => onMutate(),
    onError: (e) => onError(e),
  });

  return {
    createOrganizationMutation,
    verifyOrganizationMutation,
  };
};
