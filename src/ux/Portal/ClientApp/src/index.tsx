// declare const window: any;


document.addEventListener("DOMContentLoaded", initializeApplication);

export default function initializeApplication() {
    var settingsRequest = new XMLHttpRequest();
    settingsRequest.open("GET", "api/settings");
    settingsRequest.onreadystatechange = () => {
        if (settingsRequest.readyState === 4) {
            (window as any).initProps = JSON.parse(settingsRequest.responseText);
            require("./global-init");
        }
    };
    settingsRequest.send();
}