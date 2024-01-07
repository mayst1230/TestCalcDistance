import { CalculateService, GetDistanceQueryDefinition, GetDistanceQueryResult, OpenAPI } from "../openapi";
const { calculateGetDistance } = CalculateService;

OpenAPI.BASE = 'http://localhost:7008'
OpenAPI.HEADERS = {
  "Access-Control-Allow-Origin": "*",
}

/**Получение дистанции между двумя точками. */
export function getDistance(query: GetDistanceQueryDefinition): Promise<GetDistanceQueryResult> {
  return calculateGetDistance(query)
    .then((result) => {
      return result;
    })
    .catch((error) => {
      throw new Error(error);
    });
};
