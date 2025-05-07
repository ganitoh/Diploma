import { useMutation, useQueryClient } from "@tanstack/react-query"

import { loginUser, registration } from "@/app/http/user"

import { IDENTITY_QUERY_KEY } from "@/app/constans/query"
import { useMessage } from "../useMessage"

export const useUserMutation = () => {
    const client = useQueryClient()
    const { Success, Error, Process } = useMessage(IDENTITY_QUERY_KEY)
  
    const onSuccess = () => {
      client.invalidateQueries({ queryKey: [IDENTITY_QUERY_KEY] }).then()
      Success()
    }
  
    const onMutate = () => {
      Process()
    }
  
    const onError = (e: any) => {
      Error(e, 7)
    }
  
    const registrationMutation = useMutation({
      mutationFn: registration,
      onSuccess: () => onSuccess(),
      onMutate: () => onMutate(),
      onError: (e) => onError(e)
    })

    const loginMutation = useMutation({
      mutationFn: loginUser,
      onSuccess: () => onSuccess(),
      onMutate: () => onMutate(),
      onError: (e) => onError(e)
    })
  
    return {
      registrationMutation,
      loginMutation
    }
  }