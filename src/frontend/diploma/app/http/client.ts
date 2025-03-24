import axios from "axios";

export const organizaitonClient = axios.create({
    baseURL: process.env.NEXT_PUBLIC_ORGANIZATION_APP_API_URL,
    headers: {
      "Content-Type": "application/json",
      Accept: "application/json"
    }
})

export const identityClient = axios.create({
    baseURL: process.env.NEXT_PUBLIC_IDENTITY_APP_API_URL,
    headers: {
      "Content-Type": "application/json",
      Accept: "application/json"
    }
})