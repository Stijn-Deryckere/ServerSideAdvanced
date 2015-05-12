# Blob Storage

## Stap 1: Maak een Storage Account

* Vergeet niet om bij **Configure** alle **Monitoring options** te activeren. We zetten de **retention policy** op 5 dagen.
* Vergeet ook niet manueel om de BlobContainer public te maken (Public Container).

## Stap 2: Voeg de Azure Storage SDK toe via NuGet.

Azure Storage SDK

## Stap 2: ConnectionString toevoegen

Voeg een ConnectionString toe aan AppSettings in web.config.
Let op: dit moet in de web.config gebeuren van het project dat wordt uitgevoerd.

```
<add key="StorageConnectionString" value="DefaultEndpointsProtocol=https;AccountName={name};AccountKey={primary key}" />
```

## Stap 3: Opslaan naar BlobStorage

Vervolgens implementeren we de volgende methode. In dit geval wordt een jpg-file 
opgeslagen in de BlobStorage.

```
public String SaveImage(HttpPostedFileBase image)
{
    String fileName = Path.GetFileName(image.FileName);

    //We verschaffen onszelf toegang to de Storage Account via de ConnectionString.
    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

    //Aanmaken van de Blob client. 
    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

    //Referentie naar de container images achterhalen.
    CloudBlobContainer blobContainer = blobClient.GetContainerReference("images");

    //Referentie naar de image ophalen.
    CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);

    //We slaan het bestand eerst even lokaal op.
    String path = AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + fileName;
    image.SaveAs(path);

    //De blob met als naam de filenaam van de image aanmaken of overschrijven met een lokaal bestand.
    using(var fileStream = System.IO.File.OpenRead(path))
    {
    	blockBlob.UploadFromStream(fileStream);
    }

    //We deleten het bestand opnieuw lokaal.
    File.Delete(path);

    return fileName;
}
```

## Stap 4: Ophalen uit BlobStorage

Ophalen uit BlobStorage gaat via de gekende url-methode.