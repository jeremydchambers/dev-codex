# Azure Functions — Practice (HTTP + Service Bus)

## Series

1. [Triggers & Bindings](../azure-functions--triggers-bindings/)
2. [Hosting Plans](../azure-functions--hosting-plans/)
3. [Isolated Worker](../azure-functions--isolated-worker/)
4. [Durable Functions](../azure-functions--durable/)
5. **Practice** (this node)
6. [Best Practices](../azure-functions--best-practices/)

**Prev:** [← Durable Functions](../azure-functions--durable/) ·
**Next:** [Best Practices →](../azure-functions--best-practices/)

## What it demonstrates

One **.NET isolated** Function App with:

- an **HTTP-triggered** function (`HelloHttp`)
- a **Service Bus queue-triggered** function (`ProcessOrderMessage`)
- constructor **DI**, **`ILogger<T>`**, and config from **app settings**
- unit-tested services (logic free of the Functions host)
- a local → **Flex Consumption** publish path using **az CLI**
- **Managed Identity + RBAC** for Service Bus in Azure; connection string locally

## Key ideas

- Keep bindings in thin function classes; put rules in injectable services.
- App settings: `GreetingPrefix`, `OrdersQueueName`, and
  `ServiceBusConnection` / `ServiceBusConnection__fullyQualifiedNamespace`.
- In Azure, prefer identity-based connections over secrets.

## How to run

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download) (matches Flex `--runtime-version 10.0`)
- [Azure Functions Core Tools](https://learn.microsoft.com/azure/azure-functions/functions-run-local) (`func`)
- [Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli)
- Azurite (or another Storage emulator) for `AzureWebJobsStorage=UseDevelopmentStorage=true`
- An Azure subscription for the deploy path
- A Service Bus namespace + queue for the SB trigger (local and/or Azure)

### 1. Restore settings and run tests

From `azure-functions--practice/`:

```bash
cp AzureFunctionsPractice/local.settings.json.example AzureFunctionsPractice/local.settings.json
# Edit ServiceBusConnection (local connection string) and OrdersQueueName as needed.

dotnet test
```

PowerShell:

```powershell
Copy-Item AzureFunctionsPractice\local.settings.json.example AzureFunctionsPractice\local.settings.json
dotnet test
```

### 2. Run locally

Start Azurite (if using development storage), then:

```bash
cd AzureFunctionsPractice
func start
```

Call the HTTP function (key appears in the `func start` console):

```bash
curl "http://localhost:7071/api/hello?name=Jeremy"
```

PowerShell:

```powershell
Invoke-RestMethod "http://localhost:7071/api/hello?name=Jeremy"
```

Send a JSON message to your `orders` queue (Service Bus Explorer, SDK, or CLI). Expected body:

```json
{ "orderId": "ORD-1", "itemCount": 2 }
```

Watch the `func` console for `Service Bus order processed: ...`.

Local auth options for Service Bus:

| Approach | `local.settings.json` |
|----------|------------------------|
| Connection string (simplest locally) | `"ServiceBusConnection": "Endpoint=sb://..."` |
| Identity-based (your Entra user) | `"ServiceBusConnection__fullyQualifiedNamespace": "ns.servicebus.windows.net"` and grant yourself **Azure Service Bus Data Receiver** |

Do not commit real secrets — `local.settings.json` is gitignored.

### 3. Provision Azure (Flex Consumption + Service Bus)

Sign in and pick a Flex-capable region:

```bash
az login
az functionapp list-flexconsumption-locations --query "sort_by(@, &name)[].{Region:name}" -o table
```

PowerShell (same `az` commands):

```powershell
az login
az functionapp list-flexconsumption-locations --query "sort_by(@, &name)[].{Region:name}" -o table
```

Set variables (unique names required):

```bash
RG=rg-dev-codex-func
LOCATION=eastus2
STORAGE=stdevcodexfunc$RANDOM
APP=func-dev-codex-$RANDOM
SB_NS=sb-dev-codex-$RANDOM
QUEUE=orders
```

PowerShell:

```powershell
$RG = "rg-dev-codex-func"
$LOCATION = "eastus2"
$STORAGE = "stdevcodexfunc$(Get-Random)"
$APP = "func-dev-codex-$(Get-Random)"
$SB_NS = "sb-dev-codex-$(Get-Random)"
$QUEUE = "orders"
```

Create the resource group, storage account, Function App, and Service Bus:

```bash
az group create --name "$RG" --location "$LOCATION"

az storage account create \
  --name "$STORAGE" \
  --location "$LOCATION" \
  --resource-group "$RG" \
  --sku Standard_LRS \
  --allow-blob-public-access false

az functionapp create \
  --resource-group "$RG" \
  --name "$APP" \
  --storage-account "$STORAGE" \
  --flexconsumption-location "$LOCATION" \
  --runtime dotnet-isolated \
  --runtime-version 10.0

az servicebus namespace create \
  --resource-group "$RG" \
  --name "$SB_NS" \
  --location "$LOCATION" \
  --sku Basic

az servicebus queue create \
  --resource-group "$RG" \
  --namespace-name "$SB_NS" \
  --name "$QUEUE"
```

PowerShell:

```powershell
az group create --name $RG --location $LOCATION

az storage account create `
  --name $STORAGE `
  --location $LOCATION `
  --resource-group $RG `
  --sku Standard_LRS `
  --allow-blob-public-access false

az functionapp create `
  --resource-group $RG `
  --name $APP `
  --storage-account $STORAGE `
  --flexconsumption-location $LOCATION `
  --runtime dotnet-isolated `
  --runtime-version 10.0

az servicebus namespace create `
  --resource-group $RG `
  --name $SB_NS `
  --location $LOCATION `
  --sku Basic

az servicebus queue create `
  --resource-group $RG `
  --namespace-name $SB_NS `
  --name $QUEUE
```

### 4. Managed Identity + app settings

```bash
az functionapp identity assign --name "$APP" --resource-group "$RG"

PRINCIPAL_ID=$(az functionapp identity show \
  --name "$APP" --resource-group "$RG" --query principalId -o tsv)

SB_ID=$(az servicebus namespace show \
  --resource-group "$RG" --name "$SB_NS" --query id -o tsv)

az role assignment create \
  --role "Azure Service Bus Data Receiver" \
  --assignee-object-id "$PRINCIPAL_ID" \
  --assignee-principal-type ServicePrincipal \
  --scope "$SB_ID"

az functionapp config appsettings set \
  --name "$APP" \
  --resource-group "$RG" \
  --settings \
    GreetingPrefix=Hello \
    OrdersQueueName="$QUEUE" \
    ServiceBusConnection__fullyQualifiedNamespace="${SB_NS}.servicebus.windows.net"
```

PowerShell:

```powershell
az functionapp identity assign --name $APP --resource-group $RG

$principalId = az functionapp identity show -n $APP -g $RG --query principalId -o tsv
$sbId = az servicebus namespace show -g $RG -n $SB_NS --query id -o tsv

az role assignment create `
  --role "Azure Service Bus Data Receiver" `
  --assignee-object-id $principalId `
  --assignee-principal-type ServicePrincipal `
  --scope $sbId

az functionapp config appsettings set -n $APP -g $RG --settings `
  GreetingPrefix=Hello `
  OrdersQueueName=$QUEUE `
  "ServiceBusConnection__fullyQualifiedNamespace=$SB_NS.servicebus.windows.net"
```

### 5. Publish

```bash
cd AzureFunctionsPractice
func azure functionapp publish "$APP"
```

PowerShell:

```powershell
Set-Location AzureFunctionsPractice
func azure functionapp publish $APP
```

Get a function key and call HTTP:

```bash
KEY=$(az functionapp keys list --name "$APP" --resource-group "$RG" --query functionKeys.default -o tsv)
curl "https://${APP}.azurewebsites.net/api/hello?name=Jeremy&code=${KEY}"
```

PowerShell:

```powershell
$KEY = az functionapp keys list -n $APP -g $RG --query functionKeys.default -o tsv
Invoke-RestMethod "https://$APP.azurewebsites.net/api/hello?name=Jeremy&code=$KEY"
```

Send a queue message (portal Service Bus Explorer, or):

```bash
# Requires a suitable data-plane credential on your account
az servicebus queue send \
  --resource-group "$RG" \
  --namespace-name "$SB_NS" \
  --name "$QUEUE" \
  --body '{"orderId":"ORD-42","itemCount":1}'
```

PowerShell:

```powershell
# Requires a suitable data-plane credential on your account
az servicebus queue send `
  --resource-group $RG `
  --namespace-name $SB_NS `
  --name $QUEUE `
  --body '{"orderId":"ORD-42","itemCount":1}'
```

If `az servicebus queue send` is unavailable in your CLI version, use the portal
explorer or a small SDK script. Confirm execution in **Log stream** or Application
Insights.

### 6. Clean up

```bash
az group delete --name "$RG" --yes --no-wait
```

PowerShell:

```powershell
az group delete --name $RG --yes --no-wait
```

## Layout

| Path | Role |
|------|------|
| `AzureFunctionsPractice/Program.cs` | Isolated host builder, DI registration |
| `AzureFunctionsPractice/Functions/HelloHttp.cs` | HTTP trigger + `ILogger` |
| `AzureFunctionsPractice/Functions/ProcessOrderMessage.cs` | Service Bus trigger |
| `AzureFunctionsPractice/Services/` | Pure logic (greeting + order processing) |
| `AzureFunctionsPractice/local.settings.json.example` | Local settings template |
| `AzureFunctionsPractice.Tests/` | Unit tests for services |

## References

- [Create Flex Consumption app (CLI)](https://learn.microsoft.com/azure/azure-functions/flex-consumption-how-to#create-a-flex-consumption-app)
- [Identity-based connections tutorial (Service Bus)](https://learn.microsoft.com/azure/azure-functions/functions-identity-based-connections-tutorial-2)
- [.NET isolated worker guide](https://learn.microsoft.com/azure/azure-functions/dotnet-isolated-process-guide)
- [Service Bus trigger](https://learn.microsoft.com/azure/azure-functions/functions-bindings-service-bus-trigger)
- [Code and test Azure Functions locally](https://learn.microsoft.com/azure/azure-functions/functions-develop-local)
