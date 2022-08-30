# Create an App Service app with deployment from GitHub
# set -e # exit if error
# Variable block
location="East US"
resourceGroup="dotnet-react-app"
stagingAppServicePlan="dotnet-react-app-staging"
prodAppServicePlan="dotnet-react-app-prod"
stagingWebApp="dotnet-server-staging"
prodWebApp="dotnet-server-prod"
dbNameStaging="real-estate-db-staging"
dbNameProd="real-estate-db-prod"
keyvaultNameStaging="dotnet-app-vault-staging"
keyvaultNameProd="dotnet-app-vault-prod"
serverName="real-estate-db-server"
sku="GP_Gen5_2"
login="me-admin"

## NOTE, you need to download Azure CLI and login first using
## az login --tenant {your tenant id}

# Create a resource group.
echo "Creating $resourceGroup in "$location"..."
az group create --name $resourceGroup --location "$location"

# Create App Service plans 
echo "Creating App Service Plans + Apps"
az appservice plan create --name $stagingAppServicePlan --resource-group $resourceGroup --sku FREE
az webapp create --name $stagingWebApp --resource-group $resourceGroup --plan $stagingAppServicePlan
az webapp config appsettings set -g $resourceGroup -n $stagingWebApp --settings ASPNETCORE_ENVIRONMENT=Staging

az appservice plan create --name $prodAppServicePlan --resource-group $resourceGroup --sku s1
az webapp create --name $prodWebApp --resource-group $resourceGroup --plan $prodAppServicePlan
az webapp config appsettings set -g $resourceGroup -n $stagingWebApp --settings ASPNETCORE_ENVIRONMENT=Production

# Create staging + prod database
az postgres server create --name $serverName --resource-group $resourceGroup --location "$location" --admin-user $login --admin-password $password --sku-name $sku
az postgres db create -g $resourceGroup --server-name $serverName --name $dbName 
az postgres db create -g $resourceGroup --server-name $serverName --name $dbName 

# Create Key vaults
az keyvault create --location $location --name $keyvaultNameStaging --resource-group $resourceGroup
az keyvault create --location $location --name $keyvaultNameProd --resource-group $resourceGroup


## Further work
## - need to add access policy to keyvault for each of the web app resource based on their environment - https://docs.microsoft.com/en-us/azure/key-vault/general/assign-access-policy?tabs=azure-portal
## - add a secret called "ConnectionStrings--WebApiDatabase" with DB connection string similar to this
##  Host={host}; Database={dbName}; Username={user@$serverName}; Password={password}
## - Add firewall rule to allow IP access from local in your databases 
## run 