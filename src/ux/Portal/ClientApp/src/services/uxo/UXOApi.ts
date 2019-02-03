import axios from "axios";
import { UXOItem } from "../../models/UXO/UXOItem";
import { Configuration } from "../../tools/utils/config";
import { IFileDownload } from "../../models/Shared/IFileDownload";
import { IMapItem } from "../../models/Map/IMapItem";

export function getUXOsAsync(): Promise<IMapItem[]> {
    const url = `${Configuration.getInstance().getApiUrl()}recons/uxo/Uxo`;
    return axios
        .get(url, {})
        .then(response => {
            let results: IMapItem[] = [];
            return (Object.assign(results, response.data));
        })
        .catch(content => {
            throw new TypeError(content);
        });
}

export function getUXOAsync(uxoid: string): Promise<UXOItem> {
    const url = `${Configuration.getInstance().getApiUrl()}recons/uxo/Uxo/${uxoid}/details`;
    return axios
        .get(url, {})
        .then(response => {
            return Object.assign(new UXOItem(), response.data);
        })
        .catch(content => {
            throw new TypeError(content);
        });
}

export function generateUXODocument(uxoid: string): Promise<IFileDownload> {
    const url = `${Configuration.getInstance().getApiUrl()}recons/uxo/Uxo/${uxoid}/documents/create`;
    return axios
        .post(url, {}, {responseType: "blob"})
        .then(response => {
            const contentDisposition: string = response.headers["content-disposition"];
            const contentDispositionParts = contentDisposition.split("=");
            const filename = contentDispositionParts[1].substr(1, contentDispositionParts[1].length - 2);
            const blob = new Blob([response.data], {type: "application/octet-stream"});
            const download: IFileDownload = {
                filename: filename,
                data: blob
            };
            return download;
        })
        .catch(content => {
            throw new TypeError(content);
        });
}