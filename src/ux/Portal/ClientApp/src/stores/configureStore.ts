import { Action, createStore, applyMiddleware } from "redux";
import { IStoreState } from "./IStoreState";
import { composeWithDevTools } from "redux-devtools-extension";
import reduxImmutableStateInvariant from "redux-immutable-state-invariant";
import RootReducer from "../reducers/rootReducer";
import thunk from "redux-thunk";

export default function configureStore() {
    return createStore<IStoreState>(
        RootReducer,
        composeWithDevTools(applyMiddleware(thunk, reduxImmutableStateInvariant()))
    );
}