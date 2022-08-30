# WhatsApp-Server

This solution includes the API - It's a web service that receive and sent HTTP request and store the data on a MariaDB.


## API (Web Service)
The web service is based on WebAPI model (.NET). It works with a localDB (ProjectModels), when a controller gets a request the controller turns to the service, and the last runs the query on the DB. The queries that are intended for the contact/message controller is limited.<br />
The access is permitted just for a user that has a token (Issued at login).

#### Technologies:
- JWT
- SignalR
- Entity Framework (MariaDB)

## Run
- **The API (WebService) must run on the address: http://localhost:5156/.<br />**
- Also you need to set up the databasse: **'Update-Database' (on each part)**.<br />
- The connection string is configured to **work with MariaDB**, you have to change th username and password to connect.
- Install: JWT, SignalR, MariaDB.
