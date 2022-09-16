### Intro
This is a learning project for building a single page application (SPA) using ASP.NET and React. Most of the code comes from this [PluralSight class](https://github.com/RolandGuijt/ps-globomantics-webapi-react/tree/master/Api), other things such as dockerization, Azure deployment configuration and unit + functional tests are my own. 

### Tech-stack
- Database - Postgres
- Service - ASP.NET Core
- Service Test Framework - XUnit
- UI - ReactJS
- UI Test + Mock - Storybook

### Local Setup
#### Set up secrets
- `dotnet user-secrets init`

#### Getting Postegres Docker up
- install Dockers
- run the following command to start up postgres in docker
```
docker run --name postgresql -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=password -p 5432:5432 -v /data:/var/lib/postgresql/data -d postgres
```

- connect to it using command
```
 psql -h localhost -U admin
```

- to apply changes to db run
```
dotnet ef database update
```
or run the following command to generate migration sql script to run manually in a client
```
dotnet ef migrations script
```
**NOTE: you need to run the database update command to update database every time docker restarts, since the db image does not persist data**

#### Run Dotnet Server
- `dotnet run` - start the server locally on port 4000

#### Build Server Docker image
- build the image
```
docker build -t dotnet-server .
```

- run the image
```
docker run -it --rm -p 5000:80 --name server dotnet-server
```

### Azure Resource Create
See `azure-resource-creation.bash` for more information on the resources to create. The script generates the following resources in Azure
- Resource Group
- Database
- Key Vault
- App Service
