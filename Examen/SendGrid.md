# Mails versturen met SendGrid

## Stap 1: SendGrid account

Log in op SendGrid of maak een account aan.

## Stap 2: SendGrid package

Voeg het SendGrid-package toe aan jouw project (**business layer**).

## Stap 3: Een mail aanmaken

De volgende code laat ons toe een mail aan te maken en te zenden

```
public void SendOrderMail(Order order)
{
	//Bericht aanmaken.
    var myMessage = new SendGridMessage();

    //Instellingen maken.
    myMessage.From = new MailAddress("kristof@kristofcolpaert.com");
    String recipient = order.NewUser.Firstname + " " + order.NewUser.Name + " <" + order.NewUser.Email + ">";
    List<String> recipients = new List<String>()
    {
    	@recipient
    };
    myMessage.AddTo(recipients);
    myMessage.Subject = "Proficiat met uw bestelling!";

    //Bericht vormen.
    String message =
    	"<h3>Geachte " + order.NewUser.Firstname + "</h3>" +
    	"<p>Wij danken u voor uw eindeloos vertrouwen in onze onderneming. Hieronder kan u een overzicht vinden van de door u bestelde goederen:</p>" +
    	"<table border=\"1\"><tr><th>ID</th><th>Product</th><th>Prijs</th></tr>";
    foreach(OrderLine orderLine in order.NewOrderLines)
    {
    	message += "<tr><td>" + orderLine.NewDevice.ID + "</td><td>" + orderLine.NewDevice.Name + "</td><td>" + orderLine.RentPrice + "</td></tr>";
    }
    message += "</table><br/><p>Met vriendelijke groeten, uw Kristof Colpaert-shopverantwoordelijke</p>";
    myMessage.Html = message;

    //Bericht verzenden.
    var credentials = new NetworkCredential("kriscolp", "-Password1");
    var transportWeb = new Web(credentials);
    transportWeb.DeliverAsync(myMessage);
}
```