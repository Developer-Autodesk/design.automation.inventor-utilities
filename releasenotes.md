

# design.automation.inventor-utilities

# 1.0.0-beta4
* added new helper class NameValueMapHelper that serves as a wrapper around NameValueMap. The class is designed to provide the user with easier interface for the underlying data stored inside of the NameValueMap.  See [NameValueMapHelper page](NameValueMapHelper.md) for more information.

# 1.0.0-beta3

* modified contract for HttpOperation
	* requestOrContentFile onDemand parameter is optional for now
	* usage of this parameter (other value than empty ) will produce exception since it is not supported now
	
```
// previous call for 1.0.0-beta2 version
HttpOperation("ArgumentName", "queryArguments", "header1=value1;header2=value2", "file://output", null)

// current format
HttpOperation("ArgumentName", "queryArguments", 
	new Dictionary<string,string>(
	{
		{"header1","value1"},
		{"header2","value2"}
	}),
	"file://output")
 ```

# 1.0.0-beta2

* fixed ondemand return value
