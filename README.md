
# Design Automation for Inventor - Utility Library

![Platforms](https://img.shields.io/badge/Platform-Windows-lightgrey.svg)
![.NET](https://img.shields.io/badge/.NET%20Standard-2.0-blue.svg)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](http://opensource.org/licenses/MIT)

![Platforms](https://img.shields.io/badge/Design%20Automation-v3-green.svg)
![Platforms](https://img.shields.io/badge/Inventor-2018|2019|2020-yellow.svg)

This is package helps leverage Inventor plugin code for Design Automation. The compiled version is available on [Nuget](https://www.nuget.org/packages/Autodesk.Forge.DesignAutomation.Inventor.Utils). 

## Usage

This package is used by the [Design Automation for Inventor](https://marketplace.visualstudio.com/items?itemName=Autodesk.DesignAutomation) Visual Studio template. 

## Documentation

Please refer to [this sample](https://github.com/Developer-Autodesk/design.automation.inventor-csharp-basics) for more information. 

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

#### NameValueMapHelper class
* [NameValueMapHelper page](NameValueMapHelper.md)

## Release notes
* [release notes](releasenotes.md)

## License

This library is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT). Please see the [LICENSE](LICENSE) file for full details.

## Written by

David Obergries, [Forge Partner Development](http://forge.autodesk.com)
