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
            VALUE_TWO, 
            VALUE_THREE,
            VALUE_FOUR
        }

        [Fact]
        public void Test1()
        {
            NameValueMap nameValueMap = new NameValueMapStub();
            nameValueMap.Value["Test"] = "TestString";
            nameValueMap.Value["EnumValue"] = "VALUE_FOUR";
            nameValueMap.Value["List"] = "A, b, c, 1, 2, 3, 4, 5, 6";
            nameValueMap.Value["IntList"] = "5, 8, 9, 1, 2, 3, 4, 5, 6";
            nameValueMap.Value["EnumList"] = "VALUE_ONE, VALUe_TWO, VALUE_THREE, VALUE_FOUR";

            NameValueMapHelper nameValueMapHelper = new NameValueMapHelper(nameValueMap);
            bool success = nameValueMapHelper.TryGetValueAs("Test", out string str);
            bool s1 = nameValueMapHelper.TryGetValueAs("EnumValue", out TestEnum testEnum);
            var res1 = nameValueMapHelper.GetValuesCollection<string>("List");
            var res2 = nameValueMapHelper.GetValuesCollection<int>("IntList");
            var res3 = nameValueMapHelper.GetValuesCollection<TestEnum>("EnumList");
            Assert.Equal("TestString", str);
        }
    }
}
