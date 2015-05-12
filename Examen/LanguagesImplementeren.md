# Talen en Cultures implementeren

## Stap 1: Tabel met AvailableCultures

* Maak een tabel **AvailableCulture** aan met culturen die je wenst te gebruiken voor de webapplicatie. 
* Migreer de tabel naar de database en zorg voor content.

## Stap 2: Laat de gebruiker kiezen welke taal hij wenst

### Stap 2.1: Language Partial

Implementeer een methode in de LanguageController die de gebruiker laat kiezen uit de verschillende AvailableCultures: 

```
[HttpGet]
public ActionResult CultureChoice()
{
	List<AvailableCulture> cultures = this.CultureServ.AllCultures().ToList<AvailableCulture>();
    AvailableCulturePM culturePM;

    if (HttpContext.Request.Cookies["language"] != null)
    {
    	String value = HttpContext.Request.Cookies["language"].Value;
        AvailableCulture culture = this.CultureServ.CultureById(Convert.ToInt32(value));
        culturePM = new AvailableCulturePM()
        {
        	NewAvailableCulture = new AvailableCulture(),
            SelectAvailableCultures = new SelectList(cultures, "ID", "Name", culture.ID)
        };
    }

    else
    {
    	culturePM = new AvailableCulturePM()
        {
        	NewAvailableCulture = new AvailableCulture(),
            SelectAvailableCultures = new SelectList(cultures, "ID", "Name")
        };
    }

	return PartialView("LanguagePartial", culturePM);
}

[HttpPost]
public ActionResult CultureChoice(AvailableCulturePM culturePM)
{
	if(HttpContext.Request.Cookies["language"] == null)
    {
    	HttpCookie cookie = new HttpCookie("language");
        cookie.Expires = DateTime.Now.AddDays(5);
        cookie.Value = "" + culturePM.NewAvailableCulture.ID;
        Response.SetCookie(cookie);
	}

    else
    {
    	HttpCookie cookie = HttpContext.Request.Cookies["language"];
        cookie.Value = "" + culturePM.NewAvailableCulture.ID;
        cookie.Expires = DateTime.Now.AddDays(5);
        Response.SetCookie(cookie);
    }

    return RedirectToAction("Index", "Home");
}
```

De View: 

```
@model Language.Models.PresentationModels.AvailableCulturePM

@using (Html.BeginForm("CultureChoice", "Language", FormMethod.Post))
{
    <br />
    <div class="form-group">
        @Html.LabelFor(m => m.NewAvailableCulture)
        @Html.DropDownListFor(m => m.NewAvailableCulture.ID, Model.SelectAvailableCultures, new { @class = "form-control" })
        @Html.ValidationMessageFor(m => m.NewAvailableCulture)
    </div>
    
    <input type="submit" value="Verzenden" class="btn btn-success"/>
}
```

### Stap 2.2: De Culture hardcoded instellen

Dit kan in Global.asax, door het invoegen van de volgende methode: 

```
protected void Application_BeginRequest()
{
	if(HttpContext.Current.Request != null && 
    	HttpContext.Current.Request.Cookies["language"] != null)
    {
    	CultureInfo culture = null;

        if(HttpContext.Current.Request.Cookies["language"].Value == "1")
        {
        	culture = new CultureInfo("nl-BE");
        }

        else
        {
        	culture = new CultureInfo("en-US");
        }

        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }

    else
    {
    	CultureInfo culture = CultureInfo.CurrentCulture;
    }
}
```

## Stap 3: Aanmaken van de Resource Files voor statische info

### Stap 3.1: Modellen vertalen

```
namespace Language.Models.Models
{
    public class Device
    {
        [Required(ErrorMessageResourceType= typeof(Properties.Devices.Devices), ErrorMessageResourceName="IdError")]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Properties.Devices.Devices), ErrorMessageResourceName = "NameError")]
        [Display(Name= "Name", ResourceType= typeof(Properties.Devices.Devices))]
        public String Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Properties.Devices.Devices), ErrorMessageResourceName = "PriceError")]
        [Display(Name = "Price", ResourceType = typeof(Properties.Devices.Devices))]
        public double Price { get; set; }

        [Required(ErrorMessageResourceType = typeof(Properties.Devices.Devices), ErrorMessageResourceName = "DescriptionError")]
        [Display(Name = "Description", ResourceType = typeof(Properties.Devices.Devices))]
        public String Description { get; set; }
    }
}
``` 
### Stap 3.2: Algemene layout vertalen

```
@Html.ActionLink(@Language.Web.Properties.General.GUI.Contact
```

### Stap 3.3: Business Rules voor wisselkoersen

#### Stap 3.3.1: Wisselkoers in Resources plaatsen

Vertrek bijvoorbeeld van euro. Plaats voor elke taal in de Resources een ConvertEuroToLocal-resource:

* en-US: ConvertEuroToLocal - 1.12
* be-NL: ConvertEuroToLocal - 1

#### Stap 3.3.2: De conversie uitvoeren

De convertor doet de rest. Je kan hem makkelijk in de property onderbrengen:

```
[DataType(DataType.Currency)]
[Required(ErrorMessageResourceType = typeof(Properties.Devices.Devices), ErrorMessageResourceName = "PriceError")]
[Display(Name = "Price", ResourceType = typeof(Properties.Devices.Devices))]
private double _price;

[DataType(DataType.Currency)]
[Required(ErrorMessageResourceType = typeof(Properties.Devices.Devices), ErrorMessageResourceName = "PriceError")]
[Display(Name = "Price", ResourceType = typeof(Properties.Devices.Devices))]
public double Price
{
	get 
    { 
    	return _price * double.Parse(Properties.Devices.Devices.ConvertEuroToLocal, CultureInfo.CurrentCulture); 
    }
    set { _price = value; } //Stokeer in euro
}
```