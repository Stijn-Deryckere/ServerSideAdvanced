# Broker aanmaken

## Stap 1: Virtuele Machine aanmaken

* Maak een virtuele machine aan.
* Onder het menu **EndPoints** voorzien we toegang voor:
	* MQTT - TCP - 1883
	* RDP - TCP - 3389 (private poort)
* Connect vervolgens met de VM en stel ook daar bij de *firewallregels* poort 1883 open voor uitgaand en ingaand verkeer.

## Stap 2: Broker installeren op Windows VM

* Download en installeer Java op de machine.
* Download de broker van http://www.hivemq.com.
	* Ga in de Bin-map.
	* Run de Run-file.

## Stap 4: Voeg NuGet Package toe

Voeg het NuGet package M2Mqtt toe aan de applicaties. 

## Stap 5: Versturen van berichten naar de broker

* We maken een timer aan die elke 10 seconden wordt afgevuurd. 
* We sturen telkens de tekst Home;Kristof Colpaert;off;counter naar de subscribers.

```
public class Program
{
	public static Timer timer;
    public static MqttClient mqttClient;
    public static int counter;

    static void Main(string[] args)
    {
    	counter = 0;

        try
        {
        	mqttClient = new MqttClient("cloudpi.cloudapp.net");
            String clientId = Guid.NewGuid().ToString();
            mqttClient.Connect(clientId);
        }

        catch(Exception ex)
        {
        	Console.WriteLine("An error occurred while connecting the broker");
        }

        timer = new Timer(10000);
        timer.Elapsed += timer_Elapsed;
        timer.AutoReset = false;
        timer.Start();
    }

    static void timer_Elapsed(object sender, ElapsedEventArgs e)
    {
    	String text = "Home;Kristof Colpaert;off;" + counter;

        mqttClient.Publish("home/light", Encoding.UTF8.GetBytes(text), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Console.WriteLine("text");
        counter++;

        timer.Enabled = true;
    }
}
```

## Stap 6: Het ontvangen van berichten

```
public class Program
{
	public static MqttClient mqttClient;

    static void Main(string[] args)
    {
    	try
    	{
        	mqttClient = new MqttClient("cloudpi.cloudapp.net");
            String clientId = Guid.NewGuid().ToString();
            mqttClient.Connect(clientId);
        }

        catch(Exception ex)
        {
        	Console.WriteLine("An error occurred while connecting to the broker");
        }

        mqttClient.MqttMsgPublishReceived += mqttClient_MqttMsgPublishReceived;
        mqttClient.Subscribe(new String[] { "home/light" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    static void mqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
    	byte[] data = e.Message;

        String dataString = System.Text.Encoding.Default.GetString(data);
        Console.WriteLine(dataString);
    }
}
```