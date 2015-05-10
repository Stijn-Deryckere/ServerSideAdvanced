# UnitTest-project aanmaken

## Stap 1: References toevoegen

Voeg references toe naar: 

* ASP.NET MVC 5
* ASP.NET Identity
* Entity Framework
* Het core project
* Het business layer project

## Stap 2: Maak een file /Database/UnitTestDatabaseInitializer.cs aan.

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