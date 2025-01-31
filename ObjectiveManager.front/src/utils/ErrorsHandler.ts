import {message} from "antd";

export default class ErrorsHandler {
    static defaultErrorMessage = 'Сервис недоступен';

    static isStringNullOrEmpty(str: string | undefined): boolean {
        return str == undefined || str.trim().length === 0;
    }

    static async getErrorMessages(response: Response): Promise<string[]> {
        try {
            const message = await response.text();
            if (this.isStringNullOrEmpty(message)) {
                return [this.defaultErrorMessage];
            }

            return [message];
        } catch {
            return [this.defaultErrorMessage];
        }
    }
    
    static async showErrorMessage(error: string): Promise<void> {
        await message.error(error, 5)
    }
}