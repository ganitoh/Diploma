import { IPagingResponse, IRequestParams, IResponse } from "../models/api";
import {
  ICreateOrganiaiton,
  IOrganiaiton,
  IShortOrganiaiton,
} from "../models/organization";
import { organizaitonClient } from "./client";

export const GetPagedOrganization = async (
  params: IRequestParams
): Promise<IResponse<IPagingResponse<IOrganiaiton>>> =>
  (
    await organizaitonClient.get("/Organization/GetPagedOrganization", {
      params: {
        pageNumber: params.pageNumber,
        pageSize: params.pageSize,
      },
    })
  ).data;

export const getOrganizationByUserId = async (
  userId: string
): Promise<IResponse<IOrganiaiton>> =>
  (
    await organizaitonClient.get("/Organization/getOrganizationByUserId", {
      params: { userId },
    })
  ).data;

export const getOrganizationById = async (
  organizationId: number
): Promise<IResponse<IOrganiaiton>> =>
  (
    await organizaitonClient.get("/Organization/GetOrganizationById", {
      params: { organizationId },
    })
  ).data;

export const getTopOrganizationByRating = async (
  top: number
): Promise<IResponse<IShortOrganiaiton[]>> =>
  (
    await organizaitonClient.get("/Organization/getTopOrganizationByRating", {
      params: { top },
    })
  ).data;

export const getNotVerifyOrganization = async (): Promise<
  IResponse<IOrganiaiton[]>
> =>
  (await organizaitonClient.get("/Organization/GetNotVerifyOrganization")).data;

export const createOrganization = async (
  data: ICreateOrganiaiton
): Promise<IResponse<number>> =>
  (await organizaitonClient.post("/Organization/CreateOrganization", data))
    .data;

export const verificationOrganization = async (
  organizationId: number
): Promise<IResponse<number>> =>
  (
    await organizaitonClient.put(
      "/Organization/VerificationOrganization",
      organizationId
    )
  ).data;
