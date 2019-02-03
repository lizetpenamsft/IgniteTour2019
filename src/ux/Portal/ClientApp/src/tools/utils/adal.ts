import { AuthenticationContext, adalFetch, withAdalLogin } from "react-adal";
import { Configuration } from "./config";

export const adalConfig: any = {
    cacheLocation: "localStorage",
    clientId: Configuration.getInstance().getAadClientId(),
    tenant: Configuration.getInstance().getAadTenant(),
    instance: Configuration.getInstance().getAadInstance(),
    endpoints: {
        api: Configuration.getInstance().getAadClientId()
    }
};

export const authContext: AuthenticationContext = new AuthenticationContext(adalConfig);

export const adalApiFetch = (fetch: any, url: any, options: any) => adalFetch(authContext, adalConfig.endpoints.api, fetch, url, options);

export const withAdalLoginApi = withAdalLogin(authContext, adalConfig.endpoints.api);