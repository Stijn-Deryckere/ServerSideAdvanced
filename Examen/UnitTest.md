# UnitTest-project aanmaken

## Stap 1: References toevoegen

Voeg references toe naar: 

* ASP.NET MVC 5
* ASP.NET Identity
* Entity Framework
* Het core project
* Het business layer project

## Stap 2: Maak een file /Database/UnitTestDatabaseInitializer.cs aan

Voorzie de volgende code: 

```
public class UnitTestDatabaseInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
{
	public override void InitializeDatabase(ApplicationDbContext context)
    {
    	base.InitializeDatabase(context);
        FillData(context);
    }

    private void FillData(ApplicationDbContext context)
    {
    	//Code om database te vullen met testdata.
    }
}
```

## Stap 3: ConnectionString toevoegen

Voeg aan de App.config van het testproject een ConnectionString toe. Let op een correcte naam van de ConnectionString (bv DefaultConnection).

```
<connectionStrings>
	<add name="WineAppContext" connectionString="data source=.;Initial Catalog=UnitTestWebshop;Integrated Security=SSPI" providerName="System.Data.SqlClient" />
</connectionStrings>
```

## Stap 4: IntegrationTest aanmaken

Maak een mapje "IntegrationTests" aan en maak daarin een testklasse aan:

* [TestInitialize] zal voor elke nieuwe test de database opnieuw genereren met nieuwe dummydata. 
* [TestMethod] slaat op de daadwerkelijke testmethodes.

```
namespace Webshop.Test.Database
{
    public class UnitTestDatabaseInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        public override void InitializeDatabase(ApplicationDbContext context)
        {
            base.InitializeDatabase(context);
            FillData(context);
        }

        private void FillData(ApplicationDbContext context)
        {
            List<OS> oSs = CreateOSs().ToList<OS>();
            foreach(OS os in oSs)
            {
                context.OSs.Add(os);
            }

            List<Framework> frameworks = CreateFrameworks().ToList<Framework>();
            foreach(Framework framework in frameworks)
            {
                context.Frameworks.Add(framework);
            }

            CreateDevices(context);

            context.SaveChanges();
        }

        private IEnumerable<OS> CreateOSs()
        {
            List<OS> oSs = new List<OS>();
            for(int i = 0; i < 5; i++)
            {
                OS os = new OS()
                {
                    ID = i,
                    Name = "OS" + i
                };
                oSs.Add(os);
            }

            return oSs;
        }

        private IEnumerable<Framework> CreateFrameworks()
        {
            List<Framework> frameworks = new List<Framework>();
            for(int i = 0; i < 5; i++)
            {
                Framework framework = new Framework()
                {
                    ID = i,
                    Name = "Framework" + i
                };
                frameworks.Add(framework);
            }

            return frameworks;
        }
        
        private void CreateDevices(ApplicationDbContext context)
        {
            for(int i = 0; i < 5; i++)
            {
                Device device = new Device()
                {
                    ID = i,
                    Name = "Device" + i,
                    Description = "Device" + i,
                    Picture = "Device" + i + ".jpg",
                    PurchasePrice = 10.0,
                    RentPrice = 10.0,
                    Stock = 100,
                    DeviceOSs = new List<OS>(),
                    DeviceFrameworks = new List<Framework>()
                };

                device.DeviceOSs.Add(context.OSs.Find(i));
                device.DeviceFrameworks.Add(context.Frameworks.Find(i));

                context.Devices.Add(device);
            }
        }
    }
}
```