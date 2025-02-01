import {ObjectivesApi, Configuration, StatusesApi, AccountApi} from ".";
import AuthService from "../utils/AuthService";

class Api {
    readonly accountApi: AccountApi;
    readonly objectivesApi: ObjectivesApi;
    readonly statusesApi: StatusesApi;

    constructor(
        accountApi: AccountApi,
        objectivesApi: ObjectivesApi,
        statusesApi: StatusesApi
    ) {
        this.accountApi = accountApi;
        this.objectivesApi = objectivesApi;
        this.statusesApi = statusesApi;
    }
}

const basePath = process.env.REACT_APP_BASE_PATH

const configuration = new Configuration ({
    basePath: basePath,
    apiKey: `Bearer ${AuthService.getToken()!}`
})

const ApiClient = new Api(
    new AccountApi(configuration),
    new ObjectivesApi(configuration),
    new StatusesApi(configuration)
);

export default ApiClient;