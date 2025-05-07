import { IPagingResponse, IRequestParams, IResponse } from "../models/api";
import { ICreateProduct, IProduct, IProductShort } from "../models/product";
import { organizaitonClient } from "./client";

export const getPagedProduct = (requestParams: IRequestParams): Promise<IResponse<IProduct>> =>
    organizaitonClient.get("/GetPagedProduct", { params: { requestParams }})

export const getProductById = (productId: number): Promise<IResponse<IPagingResponse<IProduct>>> =>
    organizaitonClient.get(`/GetProductById?productId=${productId}`)

export const getTopSellingProducts = (top: number): Promise<IResponse<IProductShort[]>> =>
    organizaitonClient.get(`/GetTopSellingProducts?top=${top}`)

export const serarchProducts = (searchString: string): Promise<IResponse<IProductShort[]>> =>
    organizaitonClient.get(`/SearchProducts?searchString=${searchString}`)

export const createProduct = (productData: ICreateProduct): Promise<IResponse<IProductShort[]>> =>
    organizaitonClient.post("/CreateProduct", productData)