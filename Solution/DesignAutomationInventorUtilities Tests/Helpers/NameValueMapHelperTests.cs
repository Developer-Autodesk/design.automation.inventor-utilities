using Autodesk.Forge.DesignAutomation.Inventor.Utils.Helpers;
using Inventor;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DesignAutomationInventorUtilities_Tests.Helpers
{
    public class NameValueMapHelperTests
    {
        class TestClass
        {

        }

        enum TestEnum
        {
            VALUE_ONE = 10,
            VALUe_TWO, 
            VALUE_THREE,
            VALUE_FOUR
        }

        [Fact]
        public void Test1()
        {
            NameValueMap nameValueMap = new NameValueMapStub();
            nameValueMap.Value["Test"] = "TestString";

            NameValueMapHelper nameValueMapHelper = new NameValueMapHelper(nameValueMap);
            bool success = nameValueMapHelper.TryGetValueAs("Test", out string str);
            Assert.Equal("TestString", str);
        }

        [Fact]
        public void Test2()
        {
            /*
            NameValueMap nameValueMap = new NameValueMapStub();
            nameValueMap.Value["Test"] = "TestString";
            nameValueMap.Value["Test2"] = new TestClass();

            NameValueMapHelper nameValueMapHelper = new NameValueMapHelper(nameValueMap);

            // Collection example
            NameValueMapValue collectionValue = new NameValueMapValue("value_one value_two value_three value_four");
            var list1 = collectionValue.AsCollection();

            NameValueMapValue numericalArray = new NameValueMapValue("1,2,3,4,5,6,7,8,9,10");
            var list2 = numericalArray.AsCollection(',');

            // Enum Example
            NameValueMapValue enumValue = new NameValueMapValue("value_one");
            TestEnum e1 = enumValue.AsEnum<TestEnum>();

            // String to int and str
            NameValueMapValue strIntValue = new NameValueMapValue("196");
            bool s1 = strIntValue.TryGetAs<int>(out int outValue1);
            bool s2 = strIntValue.TryGetAs<string>(out string outValueStr1);
            Assert.Equal(196, outValue1);

            // Int to string and int
            NameValueMapValue intValue = new NameValueMapValue("2885");
            s1 = intValue.TryGetAs<int>(out int outValue2);
            s2 = intValue.TryGetAs<string>(out string outValueStr2);
            bool s3 = intValue.TryGetAs<double>(out double outValueFloat);
            Assert.Equal(2885, outValue2);

            // Floating test
            NameValueMapValue floatingValue = new NameValueMapValue("739.7889");
            s1 = floatingValue.TryGetAs<int>(out int outValue3);
            s2 = floatingValue.TryGetAs<string>(out string outValueStr3);
            s3 = floatingValue.TryGetAs<double>(out double outValueDouble);
            Assert.Equal(739.7889d, outValueDouble);

            // Bool test
            NameValueMapValue boolValue = new NameValueMapValue("true");
            s1 = boolValue.TryGetAs<bool>(out bool outBoolValue1);
            s2 = boolValue.TryGetAs<string>(out string outValueStr4);
            s3 = boolValue.TryGetAs<int>(out int outValue4);
            Assert.True(outBoolValue1);

            NameValueMapValue boolValue2 = new NameValueMapValue("1");
            s1 = boolValue2.TryGetAs<bool>(out bool outBoolValue2);
            s2 = boolValue2.TryGetAs<string>(out string outValueStr5);
            s3 = boolValue2.TryGetAs<int>(out int outValue5);

            Assert.False(outBoolValue2);
            */
        }
    }
}
