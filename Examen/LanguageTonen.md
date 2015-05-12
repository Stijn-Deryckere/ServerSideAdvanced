# Taal tonen in de browser

```
[HttpGet]
public String GetCurrentCulture()
{
	String message = "Culture: " + Thread.CurrentThread.CurrentCulture
		+ " - UICulture: " + Thread.CurrentThread.CurrentUICulture;
	return message;
}
```