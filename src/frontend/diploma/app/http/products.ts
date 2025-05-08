import { IPagingResponse, IRequestParams, IResponse } from "../models/api";
import { ICreateProduct, IProduct, IProductShort } from "../models/product";
import { organizaitonClient } from "./client";

export const getPagedProduct = (requestParams: IRequestParams): Promise<IResponse<IProduct>> =>
    organizaitonClient.get("/Product/GetPagedProduct", { params: { requestParams }})

export const getProductById = (productId: number): Promise<IResponse<IPagingResponse<IProduct>>> =>
    organizaitonClient.get(`/Product/GetProductById?productId=${productId}`)

export const getTopSellingProducts = (top: number): Promise<IResponse<IProductShort[]>> =>
    organizaitonClient.get(`/Product/GetTopSellingProducts?top=${top}`)

export const serarchProducts = (searchString: string): Promise<IResponse<IProductShort[]>> =>
    organizaitonClient.get(`/Product/SearchProducts?searchString=${searchString}`)

export const createProduct = async (productData: ICreateProduct): Promise<IResponse<IProductShort[]>> =>
    (await organizaitonClient.post("/Product/CreateProduct", productData)).data