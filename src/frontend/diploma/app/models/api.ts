export interface IResponse<T> {
  response: T
  succeeded: boolean
  message: string
}

export interface IPagingResponse<T> {
  count: number
  data: T[]
}

export interface IRequestParams {
  pageNumber?: number
  pageSize?: number
}