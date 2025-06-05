import { useQuery } from "@tanstack/react-query"
import { PRODUCT_TOP_SELLING_QUERY_KEY } from "@/app/constans/query"
import { getTopSellingProducts } from "@/app/http/products"

export const useGetTopSellingProductsQuery = (top: number) => {
  return useQuery({
      queryFn: () => getTopSellingProducts(top),
      queryKey: [PRODUCT_TOP_SELLING_QUERY_KEY, top],
      retry: false
    })
}