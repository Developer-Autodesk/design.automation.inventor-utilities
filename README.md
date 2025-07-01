
# Inventor Automation API Utility Library

![Platforms](https://img.shields.io/badge/Platform-Windows-lightgrey.svg)
![.NET](https://img.shields.io/badge/.NET%20Standard-2.0-blue.svg)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](http://opensource.org/licenses/MIT)

![Platforms](https://img.shields.io/badge/Automation%20API-v3-green.svg)
![Platforms](https://img.shields.io/badge/Inventor-grey.svg)

This is package helps leverage Inventor plugin code for the Automation Service. The compiled version is available on [Nuget](https://www.nuget.org/packages/Autodesk.Forge.DesignAutomation.Inventor.Utils).

## Usage

This package is used by the [Inventor Automation API template](https://marketplace.visualstudio.com/items?itemName=Autodesk.DesignAutomation2) Visual Studio template.

## Documentation

#### HeartBeat class

```csharp
using (new HeartBeat())
{
    // some long operation here
}
```

#### OnDemand.HttpOperation function

```csharp
// simply call the method with the parameter
OnDemand.HttpOperation(name, suffix, headers, responseFile)
```

#### NameValueMapExtension
* [NameValueMapExtension page](NameValueMapExtension.md)

## Release notes
* [release notes](releasenotes.md)

## License

This library is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT). Please see the [LICENSE](LICENSE) file for full details.

## Written by

David Obergries, [APS Partner Development](https://aps.autodesk.com/)
