/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { GetDistanceQueryDefinition } from '../models/GetDistanceQueryDefinition';
import type { GetDistanceQueryResult } from '../models/GetDistanceQueryResult';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class CalculateService {

    /**
     * Получение дистанции между двумя точками.
     * @param requestBody 
     * @returns GetDistanceQueryResult Success
     * @throws ApiError
     */
    public static calculateGetDistance(
requestBody: GetDistanceQueryDefinition,
): CancelablePromise<GetDistanceQueryResult> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/v1/Calculate/get-distance',
            body: requestBody,
            mediaType: 'application/json-patch+json',
            errors: {
                400: `Bad Request`,
            },
        });
    }

}
