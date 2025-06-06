import { useQuery } from "@tanstack/react-query"
import { checkRole, loginUser } from "@/app/http/user"
import { USER_LOGIN } from "@/app/constans/query"
import { ILoginUser } from "@/app/models/user"

export const useLoginUserQuery = (params: ILoginUser) => {
  return useQuery({
      queryFn: () => loginUser(params),
      queryKey: [USER_LOGIN, params],
      retry: false
    })
}

export const useCheckRoleUserQuery = (roleName: string) => {
  return useQuery({
      queryFn: () => checkRole(roleName),
      queryKey: [USER_LOGIN, roleName],
      retry: false
    })
}