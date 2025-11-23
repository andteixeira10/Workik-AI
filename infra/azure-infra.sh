#!/bin/bash

RESOURCE_GROUP="welltrack-rg"
LOCATION="eastus2"
SQL_SERVER_NAME="welltracksqlsrv999"
SQL_DB_NAME="WellTrackDb"
SQL_ADMIN="adminuser"
SQL_PASSWORD="Fiap@Dev2025!"
WEBAPP_NAME="welltrack-api-app"
PLAN_NAME="ASP-welltrackrg-a983"
DOCKER_IMAGE="andteixeira/welltrack-api:latest"

echo "Criando Resource Group..."
az group create --name $RESOURCE_GROUP --location $LOCATION

echo "Criando servidor SQL..."
az sql server create \
  --name $SQL_SERVER_NAME \
  --resource-group $RESOURCE_GROUP \
  --location $LOCATION \
  --admin-user $SQL_ADMIN \
  --admin-password $SQL_PASSWORD

echo "Criando Banco de Dados..."
az sql db create \
  --resource-group $RESOURCE_GROUP \
  --server $SQL_SERVER_NAME \
  --name $SQL_DB_NAME \
  --service-objective S0

MY_IP=$(curl -s ifconfig.me)

echo "Liberando firewall para IP local: $MY_IP ..."
az sql server firewall-rule create \
  --resource-group $RESOURCE_GROUP \
  --server $SQL_SERVER_NAME \
  --name AllowMyIP \
  --start-ip-address $MY_IP \
  --end-ip-address $MY_IP

echo "Criando App Service Plan..."
az appservice plan create \
  --name $PLAN_NAME \
  --resource-group $RESOURCE_GROUP \
  --is-linux \
  --sku B1

echo "Criando Web App com Docker..."
az webapp create \
  --resource-group $RESOURCE_GROUP \
  --plan $PLAN_NAME \
  --name $WEBAPP_NAME \
  --deployment-container-image-name $DOCKER_IMAGE

echo "Configurando string de conexão no Web App..."
az webapp config connection-string set \
  --resource-group $RESOURCE_GROUP \
  --name $WEBAPP_NAME \
  --settings DefaultConnection="Server=tcp:$SQL_SERVER_NAME.database.windows.net,1433;Initial Catalog=$SQL_DB_NAME;Persist Security Info=False;User ID=$SQL_ADMIN;Password=$SQL_PASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" \
  --connection-string-type SQLAzure

echo "Configuração concluída!"
