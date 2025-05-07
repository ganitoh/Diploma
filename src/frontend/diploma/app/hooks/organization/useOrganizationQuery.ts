import { useQuery } from "@tanstack/react-query"
import { USER_LOGIN } from "@/app/constans/query"
import { getOrganizationByUserId } from "@/app/http/organization"

export const useGetOrganizationByUserIdQuery = (userId: string) => {
  return useQuery({
      queryFn: () => getOrganizationByUserId(userId),
      queryKey: [USER_LOGIN, userId],
      retry: false
    })
}