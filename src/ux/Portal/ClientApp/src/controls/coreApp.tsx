import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { HashRouter } from "react-router-dom";
import App from "./app/App";
import configureStore from "../stores/configureStore";
import "../content/site.scss";
import { initializeIcons } from "office-ui-fabric-react/lib/Icons";
import { configureApiAuth, configureEsriAuthAsync } from "../services/APIGlobal";


export function initialize() {
    const configuredStore = configureStore();

    initializeIcons();

    configureApiAuth();
    // await configureEsriAuthAsync();

    const app = (
        <Provider store={configuredStore}>
            <HashRouter>
                <App />
            </HashRouter>
        </Provider>
    );
    ReactDOM.render(app, document.getElementById("root"));
}

initialize();