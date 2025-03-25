import { identityClient } from "./client";

export const confirmEmail = (email: string) : Promise<string> => identityClient.get("confirmEmail",
     { params: {email} })