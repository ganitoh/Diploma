import { useQuery } from "@tanstack/react-query";
import {
  PRODUCT_TOP_SELLING_QUERY_KEY,
  PRODUCT_GET_BY_ID_QUERY_KEY,
} from "@/app/constans/query";
import {
  getProductById,
  getShortProductByOrganization,
  getTopSellingProducts,
} from "@/app/http/products";

export const useGetTopSellingProductsQuery = (top: number) => {
  return useQuery({
    queryFn: () => getTopSellingProducts(top),
    queryKey: [PRODUCT_TOP_SELLING_QUERY_KEY, top],
    retry: false,
  });
};

export const useGetProductByIdQuery = (id: number) => {
  return useQuery({
    queryFn: () => getProductById(id),
    queryKey: [PRODUCT_GET_BY_ID_QUERY_KEY, id],
    retry: false,
  });
};

export const useGetShortProductByOrganizationQuery = (
  organizationId: number
) => {
  return useQuery({
    queryFn: () => getShortProductByOrganization(organizationId),
    queryKey: [PRODUCT_GET_BY_ID_QUERY_KEY, organizationId],
    retry: false,
  });
};
