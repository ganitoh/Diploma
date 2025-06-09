import { IResponse } from "../models/api";
import { ICreateRatingCommentary, IRating } from "../models/rating";
import { organizaitonClient } from "./client";

export const getRatingForEntity = async (
  entityId: number,
  isProduct: boolean
): Promise<IResponse<IRating>> =>
  (
    await organizaitonClient.get("/Rating/GetRatingForEntity", {
      params: { entityId, isProduct },
    })
  ).data;

export const createRating = async (
  ratingData: ICreateRatingCommentary
): Promise<IResponse<number>> =>
  (
    await organizaitonClient.post("/Rating/CreateRating", ratingData, {
      withCredentials: true,
    })
  ).data;
