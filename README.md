# WhatsApp-Server

This solution includes 2 projects:
- API - It's a web service that receive and sent HTTP request and store the data on a LocalDB.
- WebClient - this part includes the React code (inserted using the build command) and the feedback page.

## API (Web Service)
The web service is based on WebAPI model (.NET). It works with a localDB (ProjectModels), when a controller gets a request the controller turns to the service, and the last runs the query on the DB. The queries that are intended for the contact/message controller is limited.<br />
The access is permitted just for a user that has a token (Issued at login).

#### Technologies:
- JWT
- SignalR
- Entity Framework (ProjectModels)


## WebClient
The WebClient is based on the MVC model and includes 2 parts:
- Chat (Based on React) - It's the same code as the last exercise except for the changes made to communicate with the API. Each request is sent with a token (given by the API when Sign-in) and it received a signal when there is an incoming message.</br>
The link to the entire code: https://github.com/danielhouri/WhatsAppServer.</br>
- Feedback - It's the page where you can add feedback about the website.
To reach this page the user will need to press the feedback button from the home page of the Sign-in page (main page).
To add a new feedback press the button 'create new feedback button'. As mentioned the feedback site work in MVC platform, which includes: models, services, and controllers. The service uses a localDB to store the data. The site support all the 'CRUED': create, edit, details, delete. The site also supports a search function for the users who fill feedback on the website.

#### Technologies:
- JWT
- SignalR
- React
- Bootstrap
- Entity Framework (ProjectModels)

## Run
- **The API (WebService) must run on the address: https://localhost:7156/.<br />**
- Also you need to set up 2 databases: the API and the the WebClient - to do this you have to run on the console the command: **'Update-Database' (on each part)**.<br />
- The connection string is configured to **work with ProjectModels**, you can change it to MSSQL.
- Install: JWT, SignalR, and React.

## Submitting

- Daniel Houri: 314712563
- Dor Huri: 209409218

