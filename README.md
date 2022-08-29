### Intro
This is a learning project for building a single page application (SPA) using ASP.NET and React
- follow along this repo - https://github.com/RolandGuijt/ps-globomantics-webapi-react/tree/master/Api

### Local Setup
#### Set up secrets
- `dotnet user-secrets init`
- `dotnet user-secrets set "ConnectionStrings:WebApiDatabase" {use connection string from azure db}`

#### Run Dotnet Server
- `dotnet run` - start the server locally on port 4000

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
- App Service - Beta
- App Service - Prod
