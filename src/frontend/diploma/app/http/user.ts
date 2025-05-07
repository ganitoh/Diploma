import { IResponse } from "../models/api";
import { ICreateUser, ILoginUser, IUser } from "../models/user";
import { identityClient } from "./client";

export const confirmEmail = (email: string) : Promise<IResponse<string>> => identityClient.get("Identity/confirmEmail",
     { params: {email} })

export const  loginUser = async (loginData: ILoginUser) : Promise<IResponse<string>> => (await identityClient.post("Identity/login", loginData, {
     withCredentials: true,
   })).data

export const  registration = async (userData: ICreateUser) : Promise<IResponse<string>> => (await identityClient.post("Identity/registration", userData)).data