import { runWithAdal } from "react-adal";
import { Configuration } from "./tools/utils/config";
import { authContext } from "./tools/utils/adal";

runWithAdal(
    authContext,
    () => {
        require("./controls/coreApp")
    },
    Configuration.getInstance().getSkipAuth()
);