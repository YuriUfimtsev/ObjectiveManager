import {message} from "antd";
import {ResponseError} from "../api";

export default class ErrorsHandler {
    private static defaultErrorMessage = 'Сервис недоступен';

    public static isStringNullOrEmpty(str: string | undefined): boolean {
        return str == undefined || str.trim().length === 0;
    }

    public static async getErrorMessages(responseError: ResponseError): Promise<string[]> {
        try {
            const httpErrors = await responseError.response.json();
            if (this.isNotEmptyStringArray(httpErrors)) {
                return httpErrors
            }
            
            const errors = httpErrors.errors.Comment;
            if (this.isNotEmptyStringArray(errors)) {
                return errors
            }

            return [this.defaultErrorMessage];
        } catch {
            return [this.defaultErrorMessage];
        }
    }

    public static async showErrorMessage(error: string, isContinuous: boolean = false): Promise<void> {
        isContinuous
            ? await message.error(error, 0)
            : await message.error(error, 5)
    }

    private static isNotEmptyStringArray(object: any): object is string[] {
        if (!Array.isArray(object) || object.length === 0) {
            return false;
        }

        return !object.some(item => typeof item !== 'string');
    }
}