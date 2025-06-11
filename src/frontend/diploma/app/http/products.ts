import { IRequestParams, IResponse } from "../models/api";
import {
  ICreateProduct,
  IProduct,
  IProductShort,
  IUpdateProduct,
} from "../models/product";
import { organizaitonClient } from "./client";

export const getPagedProduct = (
  requestParams: IRequestParams
): Promise<IResponse<IProduct>> =>
  organizaitonClient.get("/Product/GetPagedProduct", {
    params: { requestParams },
  });

export const getProductById = async (
  productId: number
): Promise<IResponse<IProduct>> =>
  (
    await organizaitonClient.get(
      `/Product/GetProductById?productId=${productId}`
    )
  ).data;

export const getTopSellingProducts = async (
  top: number
): Promise<IResponse<IProductShort[]>> =>
  (await organizaitonClient.get(`/Product/GetTopSellingProducts?top=${top}`))
    .data;

export const serarchProducts = async (
  searchString: string
): Promise<IResponse<IProductShort[]>> =>
  (
    await organizaitonClient.get(
      `/Product/SearchProducts?searchString=${searchString}`
    )
  ).data;

export const getShortProductByOrganization = async (
  organizationId: number
): Promise<IResponse<IProductShort[]>> =>
  (
    await organizaitonClient.get("/Product/getShortProductByOrganization", {
      params: { organizationId },
    })
  ).data;

export const createProduct = async (
  productData: ICreateProduct
): Promise<IResponse<number>> =>
  (await organizaitonClient.post("/Product/CreateProduct", productData)).data;

export const updateProduct = async (
  productData: IUpdateProduct
): Promise<IResponse<number>> =>
  (await organizaitonClient.put("/Product/UpdateProduct", productData)).data;

export const deleteProduct = async (
  ids: number[]
): Promise<IResponse<number>> =>
  (
    await organizaitonClient.delete("/Product/DeleteProduct", {
      data: ids,
    })
  ).data;
