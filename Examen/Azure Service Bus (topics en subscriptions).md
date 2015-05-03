# Azure Service Bus (Topics en Subscriptions)

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

## Stap 4: ConnectionString opvragen in de code

Vervolgens halen we de ConnectionString op in de code: 

```
//Connectie maken met namespace
String connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
```

## Stap 5: NamespaceManager aanmaken en Topic aanmaken

In deze stap maken we een referentie naar de namespace aan, en creëren we een topic, indien dat nog niet bestaat. 

```
var namespaceManager = 
    NamespaceManager.CreateFromConnectionString(connectionString);

if (!namespaceManager.TopicExists("TestTopic"))
{
    namespaceManager.CreateTopic(td);
}
```

## Stap 6: Topic-settings instellen

We kunnen ook Settings instellen op het topic. Zo kunnen we bijvoorbeeld een maximale grootte van berichten meegeven en ook zeggen hoe lang een bericht maximaal mag blijven bestaan. Aangevuld met de bovenstaande code, ziet dit er als volgt uit: 

```
// Configure Topic Settings
TopicDescription td = new TopicDescription("TestTopic");
td.MaxSizeInMegabytes = 5120;
td.DefaultMessageTimeToLive = new TimeSpan(0, 1, 0);

// Create a new Topic with custom settings
string connectionString = 
    CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

var namespaceManager = 
    NamespaceManager.CreateFromConnectionString(connectionString);

if (!namespaceManager.TopicExists("TestTopic"))
{
    namespaceManager.CreateTopic(td);
}
```

## Stap 7: Subscriptions aanmaken

We hebben nu een topic aangemaakt, maar we moeten nu ook nog verschillende subscriptions aanmaken waarmee we kunnen inschrijven op berichten van dat topic.

### Stap 7.1: Een subscription met een default filter aanmaken die alle berichten opvangt

* We maken eerst connectie met de ConnectionString. 
* We maken een referentie naar de Namespace aan. 
* We kijken of een subscription reeds bestaat, en indien niet, creëren we die.

```
string connectionString = 
    CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

var namespaceManager = 
    NamespaceManager.CreateFromConnectionString(connectionString);

if (!namespaceManager.SubscriptionExists("TestTopic", "AllMessages"))
{
    namespaceManager.CreateSubscription("TestTopic", "AllMessages");
}
```
### Stap 7.2: Een subscription met een custom filter aanmaken

We kunnen subscriptions ook laten filteren. Dit gaat op de properties van het **BrokeredMessage**-bericht dat we in het Topic plaatsten. 

```
//Subscription aanmaken die berichten met als topic "Problem" accepteert
SqlFilter problemFiter = new SqlFilter("TopicID = 1");

if (!namespaceManager.SubscriptionExists("websitemessages", "Problem"))
{
	namespaceManager.CreateSubscription("websitemessages", "Problem", problemFiter);
}

//Subscription aanmaken die berichten met als topic "Question" accepteert
SqlFilter questionFilter = new SqlFilter("TopicID = 2");

if(!namespaceManager.SubscriptionExists("websitemessages", "Question"))
{
	namespaceManager.CreateSubscription("websitemessages", "Question", questionFilter);
}
```

## Stap 8: Berichten verzenden naar een Topic

* Maak een TopicClient aan. 
* Maak een BrokeredMessage aan en stel de juiste Properties in. Deze **Properties** zorgen ervoor dat we hier later op kunnen filteren. 
* Verzendt het BrokeredMessage naar het Topic.

```
//Maak een TopicClient aan.
TopicClient topicClient = TopicClient.CreateFromConnectionString(connectionString, "websitemessages");
            
//Maak een BrokeredMessage aan.
BrokeredMessage message = new BrokeredMessage(form);
message.Properties["TopicID"] = form.NewFormTopic.ID;

//Verzendt het bericht.
topicClient.Send(message);
```

## Stap 9: Aanmaken van WebJobs die berichten zullen uitlezen

* Maak voor elke **Subscription** een **consoleapplicatie** aan die de berichten binnen die subscription zal uitlezen.
* Voorzie in de **App.config** van deze consoleapplicaties een referentie naar de ConnectionString in de **<appSettings>**.

```
<appSettings>
	<add key="Microsoft.ServiceBus.ConnectionString" value="Endpoint=sb://iotshop.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=nftcZbmuxO+qqHa9APjqskij3jNjhbff/To40+NRmoU=" />
</appSettings>
```

### Stap 9.1: Schrijven klasse die berichten uit subscription zal verwerken

* De juiste **Repositories** en **Services** voorzien.
* Connectie maken met de **SubscriptionClient** via de *ConnectionString*.
* De juiste callback-opties configureren voor de berichten via **OnMessageOptions**.
* Berichten in de subscription opvangen en verwerken. 
* Berichten in de subscription handmatig verwijderen, indien er gebruik gemaakt wordt van **PeekLock**.
* Fouten opvangen.

De code hieronder toont een voorbeeld van PeekLock:

```
//Context aanmaken.
WebshopContext context = new WebshopContext();
IGenericRepository<FormTopic> formTopicRepo = new GenericRepository<FormTopic>(context);
IFormRepository formRepo = new FormRepository(context);
IFormService formServ = new FormService(formTopicRepo, formRepo);

//Connectie maken en client opvragen.
String connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
SubscriptionClient client = SubscriptionClient.CreateFromConnectionString(connectionString, "websitemessages", "Question");

//Voorbeeld van de PeekLock receive mode.

//Callback-opties configureren
OnMessageOptions options = new OnMessageOptions();
options.AutoComplete = false;
options.AutoRenewTimeout = TimeSpan.FromMinutes(1);

//Berichten in de subscription opvangen.
client.OnMessage((message) =>
{
	try
    {
    	//Bericht verwerken
        Form form = message.GetBody<Form>();

        //Bericht opslaan in de database
        formServ.SaveForm(form);
                    
        //Bericht verwijderen uit subscription
        message.Complete();
    }

    catch(Exception ex)
    {
    	//Toont aan dat er een probleem is, we unlocken het bericht in de subscription.
        message.Abandon();
    }
}, options);
```