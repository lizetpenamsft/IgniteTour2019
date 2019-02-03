import { Action } from "redux";
import { IMapItem } from "../../models/Map/IMapItem";
import keys from "../ActionTypeKeys";

export interface IGetUXOsSuccessAction extends Action {
    readonly type: keys.GETALLUXOS_SUCCESS;
    readonly payload: {
        uxos: IMapItem[];
    };
}

export interface IGetUXOsInProgressAction extends Action {
    readonly type: keys.GETALLUXOS_INPROGRESS;
}

export interface IGetUXOsUXOFailAction extends Action {
    readonly type: keys.GETALLUXOS_FAIL;
    readonly payload: {
        readonly error: Error;
    }
}