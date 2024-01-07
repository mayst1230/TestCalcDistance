/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

/**
 * Бизнесовая ошибка от API.
 */
export type ApiErrorResponse = {
    /**
     * Список ошибок, где ключ - имя параметра, а значение - текст ошибок этого параметра.
 * Если параметр string.Empty, то ошибка общая.
     */
    Errors?: Record<string, Array<string>> | null;
};
