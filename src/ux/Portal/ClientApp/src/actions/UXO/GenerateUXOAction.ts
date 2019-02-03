import { Dispatch } from "redux";
import keys from "../ActionTypeKeys";
import * as IAction from "./IGenerateUXOAction";
import { generateUXODocument } from "../../services/uxo/UXOApi";
import { IFileDownload } from "../../models/Shared/IFileDownload";

export function GenerateUXODocument(id: string): (
    dispatch: Dispatch<IAction.IGenerateUXOInProgressAction | IAction.IGenerateUXOSuccessAction | IAction.IGenerateUXOFailAction>
) => Promise<void> {
    return async (dispatch) => {
        dispatch(inProgress());
        try {
            const blob = await generateUXODocument(id);
            dispatch(success(blob));
        } catch (err) {
            dispatch(fail(err));
        }
    };
}

function inProgress(): IAction.IGenerateUXOInProgressAction {
    return {
        type: keys.GENERATEUXO_INPROGRESS
    };
}

function success(file: IFileDownload): IAction.IGenerateUXOSuccessAction {
    return {
        type: keys.GENERATEUXO_SUCCESS,
        payload: {
            file
        }
    };
}

function fail(error: Error): IAction.IGenerateUXOFailAction {
    return {
        type: keys.GENERATEUXO_FAIL,
        payload: {
            error
        }
    };
}