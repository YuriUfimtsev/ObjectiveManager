import {ObjectivesApi, Configuration, StatusesApi} from ".";

class Api {
    readonly objectivesApi: ObjectivesApi;
    readonly statusesApi: StatusesApi;

    constructor(
        objectivesApi: ObjectivesApi,
        statusesApi: StatusesApi
    ) {
        this.objectivesApi = objectivesApi;
        this.statusesApi = statusesApi;
    }
}

const basePath = process.env.REACT_APP_BASE_PATH

let ApiClient: Api;
const configuration = new Configuration ({
    basePath: basePath
})

ApiClient = new Api(
    new ObjectivesApi(configuration),
    new StatusesApi(configuration)
);

export default ApiClient;