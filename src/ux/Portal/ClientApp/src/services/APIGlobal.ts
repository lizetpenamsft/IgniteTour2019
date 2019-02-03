import axios, { AxiosError, AxiosRequestConfig } from "axios";
import { authContext, adalConfig } from "../tools/utils/adal";

export function configureApiAuth() {
    axios.defaults.headers.common["Content-Type"] = "application/json";
    axios.defaults.headers.post["Access-Control-Allow-Origin"] = "*";
    axios.defaults.headers.post["Access-Control-Allow-Methods"] = "GET,POST,PUT,DELETE,OPTIONS";
    axios.defaults.headers.post["Access-Control-Allow-Headers"] = "Content-Type";

    axios.interceptors.request.use(
        (request: AxiosRequestConfig) => {
            let originalRequest: AxiosRequestConfig;
            originalRequest = { ...request };
            return new Promise((resolve, reject) => {
                if (request.method === "OPTIONS" || request.headers.Authorization) {
                    resolve(originalRequest);
                } else {
                    authContext.acquireToken(
                        adalConfig.endpoints.api,
                        (_errorDesc: string | null, token: string | null, _error: Error) => {
                            if (!_errorDesc) {
                                originalRequest.headers.Authorization = `Bearer ${token}`;
                                resolve(originalRequest);
                            } else {
                                reject({_errorDesc, _error});
                            }
                        }
                    )
                }
            });
        },
        (error: AxiosError) => {
            return Promise.reject(error);
        }
    );
}