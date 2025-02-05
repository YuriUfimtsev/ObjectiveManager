import {ObjectivesApi, Configuration, StatusesApi, AccountApi, FrequencyApi, NotificationsApi} from ".";
import AuthService from "../utils/AuthService";

class Api {
    readonly accountApi: AccountApi;
    readonly objectivesApi: ObjectivesApi;
    readonly statusesApi: StatusesApi;
    readonly frequencyApi: FrequencyApi;
    readonly notificationsApi: NotificationsApi;

    constructor(
        accountApi: AccountApi,
        objectivesApi: ObjectivesApi,
        statusesApi: StatusesApi,
        frequencyApi: FrequencyApi,
        notificationsApi: NotificationsApi
    ) {
        this.accountApi = accountApi;
        this.objectivesApi = objectivesApi;
        this.statusesApi = statusesApi;
        this.frequencyApi = frequencyApi;
        this.notificationsApi = notificationsApi;
    }
}

const basePath = process.env.REACT_APP_BASE_PATH

const configuration = new Configuration ({
    basePath: basePath,
    apiKey: () => `Bearer ${AuthService.getToken()!}`
})

const ApiClient = new Api(
    new AccountApi(configuration),
    new ObjectivesApi(configuration),
    new StatusesApi(configuration),
    new FrequencyApi(configuration),
    new NotificationsApi(configuration)
);

export default ApiClient;