import { Dispatch } from "redux";
import keys from "../ActionTypeKeys";
import * as IAction from "./IRetrieveUXOAction";
import { getUXOAsync } from "../../services/uxo/UXOApi";
import { UXOItem } from "../../models/UXO/UXOItem";

export function RetrieveUXO(id: string): (
    dispatch: Dispatch<
        IAction.IUXOGetSuccessAction | IAction.IUXOGetInProgressAction | IAction.IUXOGetFailAction
    >
) => Promise<void> {
    return async (dispatch) => {
        dispatch(getUXOInProgress());
        try {
            let uxo = await getUXOAsync(id);
            dispatch(getUXOSuccess(uxo));
        } catch (err) {
            dispatch(getUXOFail(err));
        }
    };
}

function getUXOInProgress(): IAction.IUXOGetInProgressAction {
    return {
        type: keys.GETUXO_INPROGRESS
    };
}

function getUXOSuccess(uxo: UXOItem): IAction.IUXOGetSuccessAction {
    return {
        type: keys.GETUXO_SUCCESS,
        payload: {
            uxo
        }
    };
}

function getUXOFail(error: Error): IAction.IUXOGetFailAction {
    return {
        type: keys.GETUXO_FAIL,
        payload: {
            error
        }
    };
}