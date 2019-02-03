const CONFIG_KEYS = {
    AAD_CLIENT_ID: "aadClientId",
    AAD_TENANT: "aadTenant",
    API_ID: "apiId",
    SKIP_AUTH: "skipAuth",
    AAD_INSTANCE: "aadInstance",
    API_URL: "apiUrl",
    ICON_URL: "iconBaseUrl"
};

export class Configuration {
    private static instance: Configuration;
    private configuration: any = {};

    private constructor() {
        this.configuration = (window as any).initProps || {};
    }

    static getInstance() {
        if (!Configuration.instance) {
            Configuration.instance = new Configuration();
        }
        return Configuration.instance;
    }

    private get(key: string): any {
        return this.configuration[key];
    }

    getAadClientId(): string {
        return this.get(CONFIG_KEYS.AAD_CLIENT_ID);
    }

    getAadTenant(): string {
        return this.get(CONFIG_KEYS.AAD_TENANT);
    }

    getApiId(): string {
        return this.get(CONFIG_KEYS.API_ID);
    }

    getSkipAuth(): boolean {
        return this.get(CONFIG_KEYS.SKIP_AUTH)
    }

    getAadInstance(): string {
        return this.get(CONFIG_KEYS.AAD_INSTANCE)
    }

    getApiUrl(): string {
        return this.get(CONFIG_KEYS.API_URL)
    }

    getIconBaseUrl(): string {
        return this.get(CONFIG_KEYS.ICON_URL)
    }
}