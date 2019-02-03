import ActionTypes from "../../actions/ActionTypes";
import initialState from "../initialState";
import ActionTypeKeys from "../../actions/ActionTypeKeys";
import { saveAs } from "file-saver";

export default function UXOReducer(state = initialState.uxo, action: ActionTypes) {
    switch (action.type) {
        case ActionTypeKeys.GETUXO_INPROGRESS:
            return { ...state, isLoading: true };
        case ActionTypeKeys.GETUXO_SUCCESS:
            return { ...state, isLoading: false, selectedItem: action.payload.uxo };

        case ActionTypeKeys.GENERATEUXO_INPROGRESS:
            return { ...state, isGeneratingDocument: true };
        case ActionTypeKeys.GENERATEUXO_SUCCESS:
            saveAs(action.payload.file.data, action.payload.file.filename);
            return { ...state, isGeneratingDocument: false };

        case ActionTypeKeys.GETALLUXOS_INPROGRESS:
            return { ...state };
        case ActionTypeKeys.GETALLUXOS_SUCCESS:
            return { ...state, items: action.payload.uxos };
        default:
            return state;
    }
}