### Intro
This is a learning project for building a single page application (SPA) using ASP.NET and React
- follow along this repo - https://github.com/RolandGuijt/ps-globomantics-webapi-react/tree/master/Api

#### Set up secrets
- `dotnet user-secrets init`
- `dotnet user-secrets set "ConnectionStrings:WebApiDatabase" {value can be found in azure keyvaults under 'ConnectionStrings--WebApiDatabase}`

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