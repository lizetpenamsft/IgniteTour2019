# IgniteTour 2019 - Azure Government Demo

## Overview

This application shows an end-to-end web application of a client app talking to an API backend using secure development practices. This scenario is showing an example portal showing potential reported IED locations.

> Note: For any of the configurations below, this application is targeting Azure Government. Endpoints will have to be adjusted for Azure Commercial.

## Client Application

Authentication between the client app and the APIs are using Azure AD authentication. The client app uses the [React ADAL][react-adal] library to force initial authentication. To make it run, at a high level the following components are needed for each environment e.g. dev, prod, etc:

- Register an application for the web app in Azure AD and grab its Application ID (`webapp-{env}-id`)
- Register an application for the API app in Azure AD and grab its Application ID (`apiapp-{env}-id`)
    - Once this application is registered, in Azure AD the client application will need be adjusted to add permissions to the API app
- The URL of your API app will be needed
- The URL of a location for storing icon symbols will be needed

## API Application

The API app has endpoints authenticated using JWT. Once a call comes in and is validated it will query SQL Azure but does so going through Azure AD and using certificates to authenticate. To make it run, for each environment the following are needed:

- In Azure AD register the following IDs and save their Application IDs:
    - `api-{env}-mgmt` | `api-{env}-mgmt-id`
    - `api-{env}-sql` | `api-{env}-id`
- Create provision a Key Vault `api-{env}-kv`
    - Adjust access policies to allow `api-{env}-mgmt` to **list** and **get** secrets and certificates.
- In Key Vault, provision 2 self-signed certificates and download their .cer files and save the thumbprint and .pfx file for the **mgmt** certificate:
    - `api-{env}-mgmt-cert` | `api-{env}-mgmt-cert-cer` | `api-{env}-mgmt-cert-thumbprint` | `api-{env}-mgmt-cert-pfx`
    - `api-{env}-sql-cert` | `api-{env}-sql-cert-cer`
- In Key Vault, add the connection string for the storage account: `api-{env}-storage`
- On each machine running the API application, add the mgmt-pfx certificate to the local machine store.
    > Note: If the app is running in IIS, you may have to grant IIS_IUSRS access to that certificate
- In Azure AD upload the corresponding .cer file to the **Keys** section for the identities you created:
    - `api-{env}-mgmt` will get `api-{env}-mgmt-cert-cer`
    - `api-{env}-sql` will get `api-{env}-sql-cert-cer`
- Connect to your SQL database in SSMS using an Azure AD account. Open a new query editor and execute the following script:
    ```sql
    CREATE USER [api-{env}-sql] FROM EXTERNAL PROVIDER;
	ALTER ROLE db_datareader ADD MEMBER [api-{env}-sql];
	ALTER ROLE db_datawriter ADD MEMBER [api-{env}-sql];
    ALTER ROLE db_ddladmin ADD MEMBER [api-{env}-sql];
    ```
- Copy the connection string for your database: `sql-{env}-connection`
    > Note: Remove any username or password field from the string

## Additional notes

This demo does not have all the necessary components to run it out of the box. Specifically, some of the things missing are any of the symbols that would be associated with the map point locations. In order to run, you would have to upload your own icons and provide a location they can be accessed from.

The default database deployment does not include any sample data with it. The API application does not include any POST operations to create new data points in the sample so they will either need to be added manually in SQL or someone is free to submit a PR to add a POST endpoint in the controller :)

<!-- links -->
[typescript]: https://www.typescriptlang.org/
[react-redux]: https://redux.js.org/basics/usage-with-react
[esri-js]: https://developers.arcgis.com/javascript/latest/api-reference/index.html
[arcgis-react]: https://github.com/esri/react-arcgis
[react-adal]: https://github.com/salvoravida/react-adal
[axios]: https://github.com/axios/axios