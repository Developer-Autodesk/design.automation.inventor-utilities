# NameValueMapExtension
This extension class is designed to simplify work with the data inside of the NameValueMap by adding multiple useful extension methods.
First we have to include the extension helper class into our project:

```csharp
using Autodesk.Forge.DesignAutomation.Inventor.Utils.Helpers;
```

For this example lets consider a NameValueMap that contains these data:
```csharp
nameValueMap.Value["StringValue"] = "TestString";
nameValueMap.Value["IntValue"] = "356";
nameValueMap.Value["DoubleValue"] = "114.3998";
nameValueMap.Value["BoolValue"] = "True";
nameValueMap.Value["EnumValue"] = "VALUE_FOUR";
nameValueMap.Value["StringCollection"] = "Alpha, beta, gamma, 1, 2, 3, 4, delta, 6, longer_teeeeeextttttt";
nameValueMap.Value["IntCollection"] = "5, 8, 9, 100, 2, 3, 4096, 5, 60";
nameValueMap.Value["DoubleCollection"] = "4.2, 11.82, 9.156, 10009.45, 200.42, 30.333, 4.2, 12.0, 9.0";
nameValueMap.Value["BoolCollection"] = "True, False, FALSE, TRUE, False, true, false, true, True";
nameValueMap.Value["EnumCollection"] = "VALUE_ONE, VALUe_TWO, VALUE_THREE, VALUE_FIVE, VALUE_FOUR";
```

And custom enumerator class MyEnum
```csharp
enum MyEnum
{
    VALUE_ONE,
    VALUE_TWO,
    VALUE_THREE,
    VALUE_FOUR,
    VALUE_FIVE
}
```

```csharp
public void Example(NameValueMap nameValueMap) 
{

```

To check that the index is present in the NameValueMap you can simply call HasKey extension method on the existing NameValueMap instance

```csharp
	if (nameValueMap.HasKey("StringValue")) 
	{
		// Do something
	}
```

You can use an array of As(type) methods to simplify the process of extracting right data type from the map.

```csharp
	string myStringValue = nameValueMap.AsString("StringValue");
	int myStringValue = nameValueMap.AsInt("IntValue");
	double myStringValue = nameValueMap.AsDouble("DoubleValue");
	bool myStringValue = nameValueMap.AsBool("BoolValue");
```

You can also convert string values separated either by space or by coma into enumerable collections of desired types (As long as the string array can be converted to the that type)

```csharp
	IEnumerable<string> myStringCollection = nameValueMap.AsStringCollection("StringCollection");
	IEnumerable<int> myIntCollection = nameValueMap.AsIntCollection("IntCollection");
	IEnumerable<double> myDoubleCollection = nameValueMap.AsDoubleCollection("DoubleCollection");
	IEnumerable<bool> myBoolCollection = nameValueMap.AsBoolCollection("BoolCollection");
```

You can even convert map values to enum using AsEnum and AsEnumCollection 

```csharp
	MyEnum myEnumValue = nameValueMap.AsEnum<MyEnum>("EnumValue");
	IEnumerable<MyEnum> myEnumCollection = nameValueMap.AsEnumCollection<MyEnum>("EnumCollection");
```

To try and convert the map value to any other type, you can use TryGetValueAs and GetValuesCollection pair of generic methods 
```csharp
	bool success = TryGetValueAs("DoubleValue", out float floatValue);
	IEnumerable<float> myFloatCollection = nameValueMap.GetValueAsCollection<float>("DoubleCollection");
}
```

All the As methods and GetValuesCollection throw InvalidValueTypeException when the conversion fails or KeyNotFoundException when the key index is not present within the map. TryGetValueAs instead returns bool to indicate success/failure.