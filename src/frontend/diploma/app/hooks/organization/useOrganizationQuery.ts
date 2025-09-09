import { useQuery } from "@tanstack/react-query";
import {
  GET_TOP_ORGANIZATION_BY_RATING,
  GET_ORGANIZATION_BY_USER_ID,
  GET_ORGANIZATION_BY_ID,
  GET_NOT_VERIFY_ORGANIZATION,
  GET_PAGED_ORGANIZATION,
} from "@/app/constans/query";
import {
  getNotVerifyOrganization,
  getOrganizationById,
  getOrganizationByUserId,
  GetPagedOrganization,
  getTopOrganizationByRating,
} from "@/app/http/organization";
import { IRequestParams } from "@/app/models/api";
import { IGetPagedOrganization } from "@/app/models/organization";

export const useGetPagedOrganizationQuery = (params: IGetPagedOrganization) => {
  return useQuery({
    queryFn: () => GetPagedOrganization(params),
    queryKey: [GET_PAGED_ORGANIZATION, params],
    retry: false,
  });
};

export const useGetOrganizationByUserIdQuery = (userId: string) => {
  return useQuery({
    queryFn: () => getOrganizationByUserId(userId),
    queryKey: [GET_TOP_ORGANIZATION_BY_RATING, userId],
    retry: false,
  });
};

export const useGetTopOrganizationByRatingQuery = (top: number) => {
  return useQuery({
    queryFn: () => getTopOrganizationByRating(top),
    queryKey: [GET_ORGANIZATION_BY_USER_ID, top],
    retry: false,
  });
};

export const useGetOrganizationByIdQuery = (organizationId: number) => {
  return useQuery({
    queryFn: () => getOrganizationById(organizationId),
    queryKey: [GET_ORGANIZATION_BY_ID, organizationId],
    retry: false,
  });
};

export const useGetNotVerifyOrganizationQuery = () => {
  return useQuery({
    queryFn: () => getNotVerifyOrganization(),
    queryKey: [GET_NOT_VERIFY_ORGANIZATION],
    retry: false,
  });
};
