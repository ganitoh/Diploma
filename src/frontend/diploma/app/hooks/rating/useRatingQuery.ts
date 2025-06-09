import { useQuery } from "@tanstack/react-query";
import { GET_RATING_FOR_ENTITY } from "@/app/constans/query";
import { getProductById, getTopSellingProducts } from "@/app/http/products";
import { getRatingForEntity } from "@/app/http/rating";

export const useGetRatingForEntityQuery = (
  entityId: number,
  isProduct: boolean
) => {
  return useQuery({
    queryFn: () => getRatingForEntity(entityId, isProduct),
    queryKey: [GET_RATING_FOR_ENTITY, entityId, isProduct],
    retry: false,
  });
};
