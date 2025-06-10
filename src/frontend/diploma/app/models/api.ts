export interface IResponse<T> {
  response: T;
  succeeded: boolean;
  message: string;
}

export interface IPagingResponse<T> {
  totalCount: number;
  entities: T[];
}

export interface IRequestParams {
  pageNumber?: number;
  pageSize?: number;
}
