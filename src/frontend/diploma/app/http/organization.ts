import { IPagingResponse, IRequestParams, IResponse } from "../models/api";
import { ICreateOrganiaiton, IOrganiaiton } from "../models/organization";
import { organizaitonClient } from "./client";

export const getOrganizationByUserId = async (userId: string): Promise<IResponse<IOrganiaiton>> =>
    (await organizaitonClient.get("/Organization/getOrganizationByUserId", { params: { userId }})).data

export const createOrganization = async (data: ICreateOrganiaiton) : Promise<IResponse<number>> =>
    (await organizaitonClient.post("/Organization/CreateOrganization", data)).data