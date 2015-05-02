# Storage Queues (insert)

## Stap 1: NuGet Packages

Voeg de Azure Storage SDK toe via NuGet.
Voeg de JSON.NET package toe via NuGet.


## Stap 2: Serialiseer objecten naar een JSON-string

```
String jsonObject = JsonConvert.SerializeObject(object);
```

## Stap 3: ConnectionString opzetten

Er moet opnieuw een ConnectionString toegevoegd worden om connection te maken met de Azure Queue. Deze ConnectionString wordt geplaatst in de **web.config** van het hoofdproject. 

```
<add key="StorageConnectionString" value="DefaultEndpointsProtocol=https;AccountName=internetofthingswebshop;AccountKey=HwL9FoMqnACL6+I1q9VKL/AqonksdcIbrlbPnvfBov9kcXchAI7BruNu0aRBwFaL1W+fched7AlCyHWlKdlH5A==" />
```
De **AccountName** en **AccountKey** dienen opgehaald te worden uit Azure. De **AccountKey** is de *primary key* die in Azure wordt meegegeven.

## Stap 4: ConnectionString benaderen in de code

Vervolgens kunnen we de ConnectionString codegewijs benaderen als volgt. 

```
CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
```

## Stap 5: Een queue aanmaken

In deze stap maken we een referentie naar een queue en maken we deze queue aan indien hij nog niet bestaat.

```
//Create the queue client. 
CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

//Retrieve a reference to a queue.
CloudQueue queue = queueClient.GetQueueReference("orderqueue");

//Create the queue if it doesn't already exist.
queue.CreateIfNotExists();
```

## Stap 6: Een bericht toevoegen aan de queue

In deze laatste fase voegen we een bericht toe aan de queue (met behulp van het eerder aangemaakte JSON-object).

```
//Create a message and add it to the queue. 
CloudQueueMessage message = new CloudQueueMessage(jsonOrder);
queue.AddMessage(message);
```