# StorageQueue (get)

## Stap 1: Webjob aanmaken

Om de berichten op de queue op een goede manier te verwerken, moeten we een Azure Webjob aanmaken.

## Stap 2: ConnectionStrings toevoegen 

In het nieuwe WebJob-project voegen we als eerste drie keer de ConnectionString naar de Azure Storage Queue toe. Dit gebeurt in **<connectionStrings>** binnen *App.config*.

De respectievelijke namen zijn:

* StorageConnectionString
* AzureWebJobsDashboard
* AzureWebJobsStorage

```
<!-- The format of the connection string is "DefaultEndpointsProtocol=https;AccountName=NAME;AccountKey=KEY" -->
<!-- For local execution, the value can be set either in this config file or through environment variables -->

<add name="StorageConnectionString" connectionString="DefaultEndpointsProtocol=https;AccountName=internetofthingswebshop;AccountKey=HwL9FoMqnACL6+I1q9VKL/AqonksdcIbrlbPnvfBov9kcXchAI7BruNu0aRBwFaL1W+fched7AlCyHWlKdlH5A==" />

<add name="AzureWebJobsDashboard" connectionString="DefaultEndpointsProtocol=https;AccountName=internetofthingswebshop;AccountKey=HwL9FoMqnACL6+I1q9VKL/AqonksdcIbrlbPnvfBov9kcXchAI7BruNu0aRBwFaL1W+fched7AlCyHWlKdlH5A==" />

<add name="AzureWebJobsStorage" connectionString="DefaultEndpointsProtocol=https;AccountName=internetofthingswebshop;AccountKey=HwL9FoMqnACL6+I1q9VKL/AqonksdcIbrlbPnvfBov9kcXchAI7BruNu0aRBwFaL1W+fched7AlCyHWlKdlH5A==" />
```

Verder moet er ook nog een ConnectionString naar de eigenlijke Azure Database toegevoegd worden, aangezien we daar de berichten op de queue willen opslaan. Gebruik hiervoor dezelfde naam als de context definieert.

```
<add name="DefaultConnection" connectionString="Server=tcp:d9qcjyit2t.database.windows.net,1433;Database=iotshop;User ID=kriscolp@d9qcjyit2t;Password=-Password1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;" providerName="System.Data.SqlClient" />
```

## Stap 3: Berichten verwerken en wegschrijven naar de database

Het verwerken van de berichten gebeurt in de *Functions.cs* klasse van de WebJob. In de hoofding ervan vervangen we het woord **orderqueue** door de naam van de queue die we willen triggeren. 

```
public static void ProcessQueueMessage([QueueTrigger("orderqueue")] string message, TextWriter log)
```

Voeg nu de juiste referenties toe en deserialiseer de parameter message opnieuw naar een object van het gewenste type. 

```
Object object = JsonConvert.DeserializeObject<Object>(message);
```

Zorg nu voor referenties naar de juist *repositories* en *services*, zodat deze kunnen aangesproken om het bericht in de database te plaatsen. 

Indien de repository problemen geeft, kan ervoor gekozen worden om twee constructors toe te voegen aan die repository: 

* Constructor
* Constructor met als parameter een ApplicationDbContext

Geef die ApplicationDbContext ook mee vanuit *Functions.cs*.

```
//Repositories en services aanmaken. 
OrderRepository orderRepo = new OrderRepository(new WebshopContext());
OrderService orderServ = new OrderService(orderRepo);

//Object opslaan in database.
orderServ.SaveOrder(order);
```

## Stap 4: De volledige code

```
public static void ProcessQueueMessage([QueueTrigger("orderqueue")] string message, TextWriter log)
{
    //Object deserialiseren.
    Order order = JsonConvert.DeserializeObject<Order>(message);

    //Repositories en services aanmaken. 
    OrderRepository orderRepo = new OrderRepository(new WebshopContext());
    OrderService orderServ = new OrderService(orderRepo);

    //Object opslaan in database.
    orderServ.SaveOrder(order);
}
```