export interface IRating {
  id: number;
  value: number;
  total: number;
  commentaries: IRatingCommentary[];
}

export interface IRatingCommentary {
  id: number;
  ratingValue: number;
  commentary: string;
  createDate: string;
  userId: string;
  userName: string;
}

export interface ICreateRatingCommentary {
  ratingValue: number;
  commentary: string;
  isProduct: boolean;
  entityId: number;
}
