import { IResponse } from "../models/api";
import { ICreateUser, ILoginUser, IUser } from "../models/user";
import { identityClient } from "./client";

export const checkRole = async (
  roleName: string
): Promise<IResponse<boolean>> =>
  (await identityClient.get("/Role/CheckRole", { params: { roleName } })).data;

export const loginUser = async (
  loginData: ILoginUser
): Promise<IResponse<string>> =>
  (
    await identityClient.post("Identity/Login", loginData, {
      withCredentials: true,
    })
  ).data;

export const registration = async (
  userData: ICreateUser
): Promise<IResponse<string>> =>
  (await identityClient.post("Identity/Registration", userData)).data;

export const logout = async (): Promise<IResponse<undefined>> =>
  (await identityClient.post("Identity/Logout")).data;
