# Storage aanmaken

## Stap 1: ConnectionString

Voorzie een ConnectionString in de **App.config** van jouw project.

```
<appSettings>
	<add key="StorageConnectionString" value="DefaultEndpointsProtocol=https;AccountName=account-name;AccountKey=account-key" />
</appSettings>
```

## Stap 2: Modellen aanpassen

Zorg ervoor dat de modellen die je in de TableStorage wil plaatsen, overerven van TableEntity. Zo ben je er zeker van dat er een PartitionKey en RowKey zijn.

## Stap 3: Connectie aanmaken en bericht plaatsen

```
public static CloudStorageAccount storageAccount;
public static CloudTableClient tableClient;
public static CloudTable table;

public static void SetupStorage()
{
	storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

    tableClient = storageAccount.CreateCloudTableClient();

    table = tableClient.GetTableReference("iot");
    table.CreateIfNotExists();
}

public static void AddValue(String data)
{
	String[] dataParts = data.Split(';');
    SensorValue sensorValue = new SensorValue()
    {
    	PartitionKey = dataParts[0],
        RowKey = dataParts[3],
        Name = dataParts[1]
    };

    TableOperation insertOperation = TableOperation.Insert(sensorValue);
    table.Execute(insertOperation);
}
```