/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { GeoCord } from './GeoCord';

/**
 * Данные запроса для получения дистанции между 2 точками (мили).
 */
export type GetDistanceQueryDefinition = {
    FirstMarker?: GeoCord;
    SecondMarker?: GeoCord;
};
