import { OrderStatus } from "./order";

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

export interface IAnalyticsRequest {
  startDate?: Date;
  endDate?: Date;
  entityId: number;
}

export interface IAnalyticsOrderByStatusRequest extends IAnalyticsRequest {
  statuses: OrderStatus[];
}

export interface IDownloadDocument {
  content: any;
  fileName: string;
}
