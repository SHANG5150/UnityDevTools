# Unity Dev Tools

This package contain some of my frequently used feature while in development Unity project.

### Installation

To install this package, use the `"Add package from git URL..."` option from Unity Package Manager, and paste the link at below:

`https://github.com/SHANG5150/UnityDevTools.git`

### Features

- [Quick Play](#quick-play)
- [Symmetric-Key Encryption](#symmetric-key-encryption)
- [Secure Prefs](#secure-prefs)
- [Hardware Verification](hardware-verification)
- [Data Extraction](#data-extraction)
- [Auto Build](#auto-build)
- [Switch Scene Shortcut](#switch-scene-shortcut)
- [Async Process](#async-process)
- [Transform Resetter](#transform-resetter)
- [Screenshot](#screenshot)
- [Dont Destroy On Load](#dont-destroy-on-load)
- [Asset Utility](#asset-utility)
- [Package Exporter](#package-exporter)
- [Open Application Path](#open-application-path)

----------

### Quick Play

An Unity editor menu item `UnityMenu/Tools/Play` which quickly open first scene at editor build settings, and enter editor play mode.

### Symmetric-Key Encryption

A simple implementation that use `key` to encryt/decrypt content.

I have salted the content by 32 bytes, so it will get different encryption result every time.

``` cs
// Create encryptor.
var settings = new SymmetricKeyEncryption.Settings() { SaltLength = 32 };
var encryptor = new SymmetricKeyEncryption(settings);

// Convert content and key to byte array.
byte[] valueBytes = BitConverter.GetBytes(888);
byte[] keyBytes = Encoding.Default.GetBytes("text content");

// Use Encrypt method to encrypt content.
byte[] encryptedBytes = encryptor.Encrypt(valueBytes, keyBytes);

// Use Decrypt method to decrypt content.
byte[] decryptedValueByte = encryptor.Decrypt(encryptedBytes, keyButes);
```

A simple way to get encryptor by factory

``` cs
var encryptor = SymmetricKeyEncryption.Factory.Create();
```

### Secure Prefs

`SecurePrefs` override all API which implement by `UnityEngine.PlayerPrefs`, and encrypt value before transfer into `PlayerPrefs`.

``` cs
// Create encryption service for SecurePrefs.
ISymmetricKeyEncryption cryptoService = SymmetricKeyEncryption.Factory.Create();

// Create setting, which defind text encoder and encryptKey.
var settings = new SecurePrefs.Settings()
{
    Encoder = Encoding.Default,
    // Use device unique identifier to limit saved value only available on same device.
    EncryptKey = Encoding.Default.GetBytes(SystemInfo.deviceUniqueIdentifier)
};

// Create securePrefs object.
// UnityPlayerPrefsFacade is just a warpper for PlayerPrefs.
// You can extend IPlayerPrefs interface to save data by other ways.
var securePrefs = new SecurePrefs(settings, cryptoService, new UnityPlayerPrefsFacade());

securePrefs.SetString("key", "value");
securePrefs.Save();
```

I have implement some factory to easly create SecurePrefs object.

``` cs
// Save data by UnityEngine.PlayerPrefs, 
// and use SystemInfo.deviceUniqueIdentifier as encryption key.
SecurePrefs.Factory.Create();

// Save data as binary files.
// Use SystemInfo.deviceUniqueIdentifier as encryption key.
// You can pass filePath to indicate the file location,
// or by default save file at 
// Application.persistentDataPath + "/PlayerPrefs.dat"
SecurePrefs.Factory.CreateBinaryPrefs();
SecurePrefs.Factory.CreateBinaryPrefs(string filePath);
```

### Hardware Verification

While in development period, I have several times to be asked to build a special version App for customer in some business reason. But our App may not get authorization system completed ready. In this case, I have to write code to force limit our application only can work on specific device by `SystemInfo.deviceUniqueIdentifier`.

Hard code device unique identifier then rebuild the App once or twice will be fine. But when this requiment become a routine, I decide to find a properly approach to deal with it.

My solution is simple. I use Google spreadsheet as database, and Google Apps Script as backend to simply create a free authorization solution. Then I can simply modify the Google sheet to enable specific device withoud rebuild.

##### Prepare Simple backend

###### 1. Create Google sheet

Name the worksheet to `Auth`, and set first row as below:

-/-|       A        |   B    |    C   |          D
---|:--------------:|:------:|:------:|:--------------------:
1  |   DeviceKey    |  Tag   |IsAuthed|LatestRequestAuthDate
2  |                |        |        | 

###### 3. Create Google Apps Script

``` javascript
function doPost(e) {
  var param = e.parameter;
  
  UpdateRequestAuthDate(param.deviceKey, param.tag);
  
  var isAuthed = CheckAuthedStatus(param.deviceKey, param.tag);
  
  if (isAuthed)
  {
    return ContentService.createTextOutput("IsAuthed");
  }
  else
  {
    return ContentService.createTextOutput("IsUnathed");
  }  
}

function GetAuthSheet()
{
  var app = SpreadsheetApp.openById("Google Sheet Id");
  var authSheet = app.getSheetByName("Auth");
  
  return authSheet;
}

function UpdateRequestAuthDate(deviceKey = "none", tag = "none")
{
  var LatestRequestAuthDateColumnNum = 4;
  var authSheet = GetAuthSheet();
  var data = authSheet.getDataRange().getValues();
  var now = Utilities.formatDate(new Date(), "GMT+8", "yyyy/MM/dd/HH:mm:ss");
  var isUpdated = false;
  
  for (var i = 0; i < data.length; i++)
  {
    if (data[i][0] == deviceKey && data[i][1] == tag)
    {
      Logger.log("find i:" + i);
      var cell = authSheet.getRange(i+1, LatestRequestAuthDateColumnNum);  // The index number of row and column is begin from 1, not 0.
      
      cell.setValue(now);
      isUpdated = true;
    }
  }

  if (!isUpdated)
  {
    var data = [deviceKey, tag, false, now];
    authSheet.appendRow(data);
  }
}

function CheckAuthedStatus(deviceKey, tag)
{
  if (deviceKey == null)
  {
    return false;
  }
  
  var isAuthedIndex = 2;
  var authSheet = GetAuthSheet();
  var data = authSheet.getDataRange().getValues();

  for (var i = 0; i < data.length; i++)
  {
    if (data[i][0] == deviceKey && data[i][1] == tag)
    {
      return data[i][isAuthedIndex];
    }
  }
  
  return false;
}
```

You need to modify Google sheet Id by you owned file.

``` javascript
// Replace Google Sheet Id by you owned file id.
SpreadsheetApp.openById("Google Sheet Id");
```

when you open a Google sheet, the URL may like this:
`https://docs.google.com/spreadsheets/d/1clr_hxgNbtiiIXXXXbmXXXXXQ5hbyk6GCmQ/edit#gid=0`
The part of `1clr_hxgNbtiiIXXXXbmXXXXXQ5hbyk6GCmQ` is the id of your sheet.

###### 4. Deploy Google Apps Script as Web App

Set `Execute the app as:` option to your own Google account.
Set `Who has access to the app:` option to `Anyone even anonymous`.

Then publish this App. At the first time you publish this App, Google need to authorize this App to access your Google sheet. Simply accept it is be fine.

###### 5. Create Unity OnlineAuthorizationSettings

Create settings asset from: `Assets/Create/DevTools/OnlineAuthorizationSettings`.

fill up settings as below:

Field          | Value
---------------|-----------------------------------------
ID             | ExampleSettings
Auth Url       | Your own Google Apps Script publish URL
Ok Response    | IsAuthed
Key Field Name | deviceKey
Tag Field Name | tag
Tag            | DevToolsExample

###### 6. Use OnlineAuthorization to Test

``` cs
// Drag and drop settings asset at inspector field.
[SerializeField]
private OnlineAuthorization.Settings settings;

private static async void LogAuthorizationStatus()
{
    // Use device unique identifier as key that backend can recognize different device later. 
    settings.Key = SystemInfo.deviceUniqueIdentifier;
    var auth = new OnlineAuthorization(authSetting.authSettings);

    bool isOk = await auth.VerifyAsync();

    Debug.Log($"Online verification status: [{isOk}]");
}
```

When first time execute `LogAuthorizationStatus()` will log `false` at console.

###### 7. Manually Enable Device Authorization Status

After first time execute the verification process, go back to see your own `Auth` sheet.

New record will added as below:

-/-|       A        |       B       |    C   |          D
---|:--------------:|:-------------:|:------:|:--------------------:
1  |   DeviceKey    |      Tag      |IsAuthed|LatestRequestAuthDate
2  |d579dd292464d70f|DevToolsExample|  FALSE |2020/11/10/15:37:57

Now you can manually edit the IsAuthed Field value to `TRUE`.

Next time, when you execute `LogAuthorizationStatus()` will log `true` at console, and you can simply use this return value to do what you want.

###### Limitation Note

Due to the limitation of free Google account, it is not recommend to use this solution for formal project. I only use this solution for temporary situation.

Please refer to [Quotas for Google Services](https://developers.google.com/apps-script/guides/services/quotas) for further understanding of Google Apps Service limitation.

### Data Extraction

I use `DataExtractor` class as a generic extraction interface to handle different kind of extraction process.

Usage:
``` cs
// Create a core processor
var excelXmlProcessor = new ExcelXmlExtractionProcessor(
                              xmlTextAsset, containerScriptableObject);

// Create DataExtractor
var extractor = new DataExtractor(excelXmlProcessor);

// Do extraction
extractor.Extract<SampleXmlData, SampleXmlData.Data>(
            (container, data) => container.Construct(data));

AssetDatabase.SaveAssets();
```

`ExcelXmlExtractionProcessor` defines how extraction process realy work. In this processor, it require a `TextAsset` object which container xml text in Microsoft Excel Xml format, and a `ScriptableObject` which is a data container.

`ExcelXmlExtractionProcessor` internally use .NET Reflaction freatures to automatically mapping the first row of Excel sheet as field names, and then parses remainder rows as a data array.

When invoke `Extract` from data extractor, it require to assign a `Fill` method to indicate how to compose row data into container.

You can implement other kind of extraction processor to extend for other source format, just simply implement `IDataExtraction` interface and pass it into DataExtractor.

### Auto Build

I have encounter a business requirement which ask me to build App with different configurations.

So I create a simple build pipeline, and separate each build details as scriptable objects that will let me combine these details to full pipeline later when the business requirement change again.

Usage:
Create pipeline settings from:
`Assets/Create/DevTools/AutoBuild/Pipeline Settings`

Then assign a Id for this settings, and select which build process you want.

Currently, I have implement several build processes, you can create it from:
`Assets/Create/DevTools/AutoBuild/Processes/...`

###### Porcesses:
- Switch Platform
    Switch to target platform.
- Build Addressables Bundle
    Simply invoke Addressables to clear build bundle.
    This process located in package sample `Addressables Extension`.
- Build Player
    Build App with different settings.

After you setup the build pipeline settings, you can click the `Build` button on the scriptable object to trigger building process.

Or you can use `AutoBuild` to run auto build process with Id.

``` cs
AutoBuild.Factory.Create().Build("Pipeline Settings Id");
```

### Switch Scene Shortcut

This feature helps to quickly open scenes with Additive stack.

Create settings form:
`Assets/Create/DevTools/SceneStack Settings`

- Id
    An id string for identifying different settings.
- Single field
    Scene reference in this field will open by `OpenSceneMode.Single`.
- Additive Scenes
    Scene reference in this list will open by `OpenSceneMode.Additive`.

After setup `SceneStack` settigns, you can use `SceneStack.Factory` to open scene stack with Id.

``` cs
SceneStack.Factory.Create("Id").OpenSceneStack();
```

### Async Process

`AsyncProcess` class provide abilities to use Unity coroutine system in class which is not inherit from `MonoBehaviour`.

``` c#
IAsyncProcess asyncProcess = AsyncProcess.Factory.Create(string name, Transform parent);
IAsyncProcess asyncProcess = AsyncProcess.Factory.CreateDontDestroy(string name, Transform parent);
IAsyncProcess asyncProcess = AsyncProcess.Factory.Create(GameObject target);
```

###### Factory method description:
- `AsyncProcess.Factory.Create(string name, Transform parent);`
    This method will create a new GameObject in active scene. Coroutine start from this interface will stop when scene has been changed.
    `Name` and `parent` are optional.
- `AsyncProcess.Factory.CreateDontDestroy(string name, Transform patent);`
    This method will create a new GameObject and set it as `DontDestroyOnLoad` object.
    `Name` and `parent` are optional.
- `AsyncProcess.Factory.Create(GameObject target)`
    This method will add coroutine service provider as a component on `target` object. The lift cycle of coroutine method will rely on the `target` object.

### Transform Resetter

`TransformResetter` class samples transform data and keeps it for reset later.

### Screenshot

`Screenshot` class save screenshot at default folder:
`Application.persistentDataPath + "/Screenshots"` 
and name the image like:
`image_2020_11_19_093044.PNG`

You can specify other folder path while construct `Screenshot` object.

``` cs
var folderPath = Path.Combine(Environment.CurrentDirectory, "Screenshots");
var screenshot = new Screenshot(folderPath);
```

### Dont Destroy On Load

`DontDestroyOnLoad` is a simple component which can add on GameObject to set the GameObject literally.

### Asset Utility

Some editor only static method which dedicate to handle assets.

- `AssetUtility.FindScriptableObjectAssets<T>(Predicate<T> match)`
  Use generic type to find scriptable object assets in project.

### Package Exporter

Create `PackageExportSettings` scriptable object from:
`Assets/Create/DevTools/Package Export Settings`

###### Setup the package export settings:
- Id
    Setup id for later usage to filter out other settings.
- Export Info
  - Package Name
      The file name of exported package.
  - Include Project Version
      Check this option to append project version text in exported package file name.
  - Package Assets Path
      A list to indicate what assets should export by this settings.
  - Open Folder After Export
      Check this option to open the output folder after export package.

###### Usage:

After setup the `Package Export Settings`, click `Export` button on the inspector of `Package Export Settings` to trigger export process.

Or run the export process from script like below.

``` cs
PackageExport.Export("Setting id");
```

### Open Application Path

A shortcut to open `Application.xxxPath` folders on Unity menu item.

Includes:
```
MenuItem/Tools/Open Application Path/Data Path
MenuItem/Tools/Open Application Path/Persistent Data Path
MenuItem/Tools/Open Application Path/Console Log Path
MenuItem/Tools/Open Application Path/Temporary Cache Path
```
