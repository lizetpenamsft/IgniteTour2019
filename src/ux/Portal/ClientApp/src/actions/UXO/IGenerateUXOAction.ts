import { Action } from "redux";
import keys from "../ActionTypeKeys";
import { IFileDownload } from "../../models/Shared/IFileDownload";

export interface IGenerateUXOSuccessAction extends Action {
    readonly type: keys.GENERATEUXO_SUCCESS;
    readonly payload: {
        readonly file: IFileDownload;
    }
}

export interface IGenerateUXOInProgressAction extends Action {
    readonly type: keys.GENERATEUXO_INPROGRESS;
}

export interface IGenerateUXOFailAction extends Action {
    readonly type: keys.GENERATEUXO_FAIL;
    readonly payload: {
        readonly error: Error;
    }
}