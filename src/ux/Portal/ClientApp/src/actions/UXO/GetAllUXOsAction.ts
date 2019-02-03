import { Dispatch } from "redux";
import keys from "../ActionTypeKeys";
import * as IAction from "./IGetAllUXOsAction";
import { getUXOsAsync } from "../../services/uxo/UXOApi";
import { IMapItem } from "../../models/Map/IMapItem";

export function GetAllUXOs(): (
    dispatch: Dispatch<IAction.IGetUXOsInProgressAction | IAction.IGetUXOsSuccessAction | IAction.IGetUXOsUXOFailAction>
) => Promise<void> {
    return async (dispatch) => {
        dispatch(inProgress());
        try {
            const items = await getUXOsAsync();
            dispatch(success(items));
        } catch (err) {
            dispatch(fail(err));
        }
    }
}

function inProgress(): IAction.IGetUXOsInProgressAction {
    return {
        type: keys.GETALLUXOS_INPROGRESS
    };
}

function success(uxos: IMapItem[]): IAction.IGetUXOsSuccessAction {
    return {
        type: keys.GETALLUXOS_SUCCESS,
        payload: {
            uxos
        }
    };
}

function fail(error: Error): IAction.IGetUXOsUXOFailAction {
    return {
        type: keys.GETALLUXOS_FAIL,
        payload: {
            error
        }
    };
}