import { ObjectivesApi, Configuration } from ".";

class Api {
    readonly objectivesApi: ObjectivesApi;

    constructor(
        objectivesApi: ObjectivesApi,
    ) {
        this.objectivesApi = objectivesApi;
    }
}

const basePath = process.env.REACT_APP_BASE_PATH

let ApiClient: Api;
const configuration = new Configuration ({
    basePath: basePath
})

ApiClient = new Api(
    new ObjectivesApi(configuration),
);
export default ApiClient;

