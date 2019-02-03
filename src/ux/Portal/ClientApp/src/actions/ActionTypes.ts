import {
    IUXOGetFailAction,
    IUXOGetInProgressAction,
    IUXOGetSuccessAction
} from "./UXO/IRetrieveUXOAction";

import {
    IGenerateUXOFailAction,
    IGenerateUXOInProgressAction,
    IGenerateUXOSuccessAction
} from "./UXO/IGenerateUXOAction";

import {
    IGetUXOsInProgressAction,
    IGetUXOsSuccessAction,
    IGetUXOsUXOFailAction
} from "./UXO/IGetAllUXOsAction";

type ActionTypes =
    | IUXOGetFailAction
    | IUXOGetInProgressAction
    | IUXOGetSuccessAction
    | IGenerateUXOFailAction
    | IGenerateUXOInProgressAction
    | IGenerateUXOSuccessAction
    | IGetUXOsInProgressAction
    | IGetUXOsSuccessAction
    | IGetUXOsUXOFailAction;

export default ActionTypes;