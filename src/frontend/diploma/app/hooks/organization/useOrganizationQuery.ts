import { useQuery } from "@tanstack/react-query"
import { GET_TOP_ORGANIZATION_BY_RATING, GET_ORGANIZATION_BY_USER_ID } from "@/app/constans/query"
import { getOrganizationByUserId, getTopOrganizationByRating } from "@/app/http/organization"

export const useGetOrganizationByUserIdQuery = (userId: string) => {
  return useQuery({
      queryFn: () => getOrganizationByUserId(userId),
      queryKey: [GET_TOP_ORGANIZATION_BY_RATING, userId],
      retry: false
    })
}

export const useGetTopOrganizationByRatingQuery = (top: number) => {
  return useQuery({
      queryFn: () => getTopOrganizationByRating(top),
      queryKey: [GET_ORGANIZATION_BY_USER_ID, top],
      retry: false
    })
}