import { combineReducers } from "redux";
import { IStoreState } from "../stores/IStoreState";

import uxo from "./uxo/UXOReducer";

const RootReducer = combineReducers<IStoreState>({
    uxo
} as any);

export default RootReducer;