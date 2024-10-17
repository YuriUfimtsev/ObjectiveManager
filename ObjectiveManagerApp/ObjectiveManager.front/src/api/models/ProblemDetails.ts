/* tslint:disable */
/* eslint-disable */
/**
 * ObjectiveManager.Api
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface ProblemDetails
 */
export interface ProblemDetails {
    [key: string]: any | any;
    /**
     * 
     * @type {string}
     * @memberof ProblemDetails
     */
    type?: string | null;
    /**
     * 
     * @type {string}
     * @memberof ProblemDetails
     */
    title?: string | null;
    /**
     * 
     * @type {number}
     * @memberof ProblemDetails
     */
    status?: number | null;
    /**
     * 
     * @type {string}
     * @memberof ProblemDetails
     */
    detail?: string | null;
    /**
     * 
     * @type {string}
     * @memberof ProblemDetails
     */
    instance?: string | null;
}

/**
 * Check if a given object implements the ProblemDetails interface.
 */
export function instanceOfProblemDetails(value: object): value is ProblemDetails {
    return true;
}

export function ProblemDetailsFromJSON(json: any): ProblemDetails {
    return ProblemDetailsFromJSONTyped(json, false);
}

export function ProblemDetailsFromJSONTyped(json: any, ignoreDiscriminator: boolean): ProblemDetails {
    if (json == null) {
        return json;
    }
    return {
        
            ...json,
        'type': json['type'] == null ? undefined : json['type'],
        'title': json['title'] == null ? undefined : json['title'],
        'status': json['status'] == null ? undefined : json['status'],
        'detail': json['detail'] == null ? undefined : json['detail'],
        'instance': json['instance'] == null ? undefined : json['instance'],
    };
}

export function ProblemDetailsToJSON(value?: ProblemDetails | null): any {
    if (value == null) {
        return value;
    }
    return {
        
            ...value,
        'type': value['type'],
        'title': value['title'],
        'status': value['status'],
        'detail': value['detail'],
        'instance': value['instance'],
    };
}

