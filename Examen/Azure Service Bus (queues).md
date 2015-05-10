# Azure Service Bus (Queues)

## Stap 1: Azure Portal

* Log in op de Azure Portal. 
* Onder het menu Service Bus moeten we eerst een nieuwe namespace aanmaken.
* Eenmaal de Service Bus is aangemaakt, kunnen we de gewenste **Topics** en **Subscriptions** aanmaken.

## Stap 2: Azure Service Bus package installeren

Installeer het Azure Service Bus Package in het project via NuGet.

## Stap 3: ConnectionString aanmaken

* Haal de ConnectionString op vanop de Azure Portal. 
* Plaats de ConnectionString nu in de **App.config** of **Web.config** in de **<appSettings>** van de applicatie waarin je hem wil gebruiken.
* Plaats de ConnectionString in de eerste plaats in het webproject.

```
<appSettings>
    <!-- Service Bus specific app setings for messaging connections -->
    <add key="Microsoft.ServiceBus.ConnectionString" value="Endpoint=sb://iotshop.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=nftcZbmuxO+qqHa9APjqskij3jNjhbff/To40+NRmoU=" />
</appSettings>
```

## Stap 4: Queue aanmaken

We vragen nu de ConnectionString op in de code en maken de queue aan, indien deze nog niet bestaat.

```
//Connectie maken met namespace
String connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

//Indien queue nog niet bestaat, aanmaken
if(!namespaceManager.QueueExists("IOTQueue"))
{
	namespaceManager.CreateQueue("IOTQueue");
}
```

## Stap 5: Queue-settings instellen

We kunnen nu een aantal instellingen op de queue zetten. We gaan bijvoorbeeld de maximale grootte van de queue instellen alsook de time-to-live van een bericht. Met het vorige erbij ziet dit er als volgt uit: 

```
//Queue settings instellen
QueueDescription queueDescription = new QueueDescription("IOTQueue");
queueDescription.MaxSizeInMegabytes = 5120;
queueDescription.DefaultMessageTimeToLive = new TimeSpan(0, 10, 0);

//Connectie maken met namespace
String connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

//Indien queue nog niet bestaat, aanmaken
if(!namespaceManager.QueueExists("IOTQueue"))
{
	namespaceManager.CreateQueue(queueDescription);
}
```

## Stap 6: Berichten verzenden naar de queue

Nu zijn we klaar om berichten te verzenden naar de queue. Dit kan door een **QueueClient** aan te maken en er **BrokeredMessages** naar te sturen. De message properties laten ons toe om later hierop te filteren by subscriptions.

```
//QueueClient aanmaken. 
QueueClient client = QueueClient.CreateFromConnectionString(connectionString, "IOTQueue");

BrokeredMessage message = new BrokeredMessage(form);
message.Properties["TopicID"] = form.NewFormTopic.ID;
client.send(message);
```

## Stap 7: Berichten van de queue opvragen

* Maak een QueueClient aan.
* Er is een mogelijkheid tot het meegeven van opties.
	* Door AutoComplete op true te zetten, activeren we ReceiveAndDelete.
	* AutoRenewTimeout geeft aan na hoeveel tijd een bericht opnieuw unlocked mag worden.

```
String connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
QueueClient client = QueueClient.CreateFromConnectionString(connectionString, "IOTQueue");

OnMessageOptions options = new OnMessageOptions();
options.AutoComplete = false;
options.AutoRenewTimeout = TimeSpan.FromMinutes(1);
```

Vervolgens kunnen we als volgt wachten op berichten:

```
Client.OnMessage((message) => {
	try
	{
		Object object = message.GetBody<Object>();
		Console.WriteLine(message.Properties["TopicID"]);

		message.Complete();
	}

	catch(Exception ex)
	{
		message.Abandon();
	}
}, options);
```