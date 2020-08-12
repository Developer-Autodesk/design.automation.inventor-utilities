
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
This optional class is designed to simplify work with the data inside the NameValueMap. 

For this example lets consider a NameValueMap that contains these data:
```csharp
map.Value["StringValue"] = "TestString";
map.Value["IntValue"] = "356";
map.Value["DoubleValue"] = "114.3998";
map.Value["BoolValue"] = "True";
map.Value["EnumValue"] = "VALUE_FOUR";
map.Value["StringCollection"] = "Alpha, beta, gamma, 1, 2, 3, 4, delta, 6, longer_teeeeeextttttt";
map.Value["IntCollection"] = "5, 8, 9, 100, 2, 3, 4096, 5, 60";
map.Value["DoubleCollection"] = "4.2, 11.82, 9.156, 10009.45, 200.42, 30.333, 4.2, 12.0, 9.0";
map.Value["BoolCollection"] = "True, False, FALSE, TRUE, False, true, false, true, True";
map.Value["EnumCollection"] = "VALUE_ONE, VALUe_TWO, VALUE_THREE, VALUE_FIVE, VALUE_FOUR";
```

```csharp
public void Example(NameValueMap map) 
{
	// NameValueMapHelper will never modify the content of the source NameValueMap
	NameValueMapHelper mapHelper = new NameValueMapHelper(map);

```

To check that the index is present you can simply call HasKey on the helper insantce

```csharp
	if (mapHelper.HasKey("StringValue")) 
	{
		// Do something
	}
```

You can now use an array of As(type) methods to simplify the process of extracting right data type from the map.

```csharp
	string myStringValue = mapHelper.AsString("StringValue");
	int myStringValue = mapHelper.AsInt("IntValue");
	double myStringValue = mapHelper.AsDouble("DoubleValue");
	bool myStringValue = mapHelper.AsBool("BoolValue");
```

You can also convert string values separated either by space or by coma into enumerable collections of desired types (As long as the string array can be converted to the desired type)

```csharp
	IEnumerable<string> myStringCollection = mapHelper.AsStringCollection("StringCollection");
	IEnumerable<int> myIntCollection = mapHelper.AsInt("IntCollection");
	IEnumerable<double> myDoubleCollection = mapHelper.AsDouble("DoubleCollection");
	IEnumerable<bool> myBoolCollection = mapHelper.AsBool("BoolCollection");
```

You can even convert map values to enum using AsEnum and AsEnumCollection 

```csharp
	MyEnum myEnumValue = mapHelper.AsEnum<MyEnum>("EnumValue");
	IEnumerable<MyEnum> myEnumCollection = mapHelper.AsEnumCollection<MyEnum>("EnumCollection");
```

To try and convert the map value to any other type, you can use TryGetValueAs and GetValuesCollection pair of generic methods 
```csharp
	bool success = TryGetValueAs("DoubleValue", out float floatValue);
	IEnumerable<float> myFloatCollection = mapHelper.GetValuesCollection<float>("DoubleCollection");
}
```

All the As methods and GetValuesCollection throw InvalidValueTypeException when the conversion fails or KeyNotFoundException when the key index is not present int he map. TryGetValueAs instead returns bool to indicate success/failure.

## Release notes
* [release notes](releasenotes.md)

## License

This library is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT). Please see the [LICENSE](LICENSE) file for full details.

## Written by

David Obergries, [Forge Partner Development](http://forge.autodesk.com)
