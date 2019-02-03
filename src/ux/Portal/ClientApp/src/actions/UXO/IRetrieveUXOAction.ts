import { Action } from "redux";
import { UXOItem } from "../../models/UXO/UXOItem";
import keys from "../ActionTypeKeys";

export interface IUXOGetSuccessAction extends Action {
    readonly type: keys.GETUXO_SUCCESS;
    readonly payload: {
        readonly uxo: UXOItem;
    };
}

export interface IUXOGetInProgressAction extends Action {
    readonly type: keys.GETUXO_INPROGRESS;
}

export interface IUXOGetFailAction extends Action {
    readonly type: keys.GETUXO_FAIL;
    readonly payload: {
        readonly error: Error;
    };
}