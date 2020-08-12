/////////////////////////////////////////////////////////////////////
// Copyright (c) Autodesk, Inc. All rights reserved
// Written by Forge Partner Development
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
/////////////////////////////////////////////////////////////////////
using Autodesk.Forge.DesignAutomation.Inventor.Utils.Helpers;
using Inventor;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace DesignAutomationInventorUtilities_Tests.Helpers
{
    enum TestEnum
    {
        VALUE_ONE = 10,
        VALUE_TWO,
        VALUE_THREE,
        VALUE_FOUR,
        VALUE_FIVE
    }

    public class TestDataProvider : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            NameValueMap nameValueMap = new NameValueMapStub();
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

            Dictionary<string, object> expectedResults = new Dictionary<string, object>()
                {
                    {"StringValue", "TestString"},
                    {"IntValue", 356},
                    {"DoubleValue", 114.3998d},
                    {"BoolValue", true},
                    {"EnumValue", TestEnum.VALUE_FOUR},

                    {"StringCollection", new List<string> {"Alpha", "beta", "gamma", "1", "2", "3", "4", "delta", "6", "longer_teeeeeextttttt"} },
                    {"IntCollection", new List<int> {5, 8, 9, 100, 2, 3, 4096, 5, 60} },
                    {"DoubleCollection", new List<double> {4.2d, 11.82d, 9.156d, 10009.45d, 200.42d, 30.333d, 4.2d, 12.0d, 9.0d} },
                    {"BoolCollection", new List<bool> {true, false, false, true, false, true, false, true, true} },
                    {"EnumCollection", new List<TestEnum> { TestEnum.VALUE_ONE, TestEnum.VALUE_TWO, TestEnum.VALUE_THREE, TestEnum.VALUE_FIVE, TestEnum.VALUE_FOUR } }

                };

            yield return new object[] { new NameValueMapHelper(nameValueMap), expectedResults };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class NameValueMapHelperTests
    {
        [Fact]
        public void ExceptionsTest()
        {
            NameValueMap nameValueMap = new NameValueMapStub();
            nameValueMap.Value["StringValue"] = "Test String";

            NameValueMapHelper mapHelper = new NameValueMapHelper(nameValueMap);

            Assert.Throws<KeyNotFoundException>(() => mapHelper.AsString("WrongIndex"));
            Assert.Throws<InvalidValueTypeException>(() => mapHelper.AsInt("StringValue"));
            Assert.Throws<KeyNotFoundException>(() => mapHelper.AsStringCollection("WrongIndex"));
            Assert.Throws<InvalidValueTypeException>(() => mapHelper.AsIntCollection("StringValue"));
        }

        [Fact]
        public void TestHasValue()
        {
            NameValueMap nameValueMap = new NameValueMapStub();
            nameValueMap.Value["StringValue"] = "TestString";
            nameValueMap.Value["IntValue"] = "356";
            nameValueMap.Value["DoubleValue"] = "114.3998";
            nameValueMap.Value["BoolValue"] = "True";
            nameValueMap.Value["EnumValue"] = "VALUE_FOUR";

            NameValueMapHelper mapHelper = new NameValueMapHelper(nameValueMap);

            Assert.True(mapHelper.HasKey("StringValue"));
            Assert.True(mapHelper.HasKey("IntValue"));
            Assert.True(mapHelper.HasKey("DoubleValue"));
            Assert.True(mapHelper.HasKey("BoolValue"));
            Assert.False(mapHelper.HasKey("NonExistantKey"));
            Assert.True(mapHelper.HasKey("EnumValue"));
        }

        [Fact]
        public void ConverterTest()
        {
            DataConverter dataConverter = new DataConverter();

            // String
            Assert.True(dataConverter.TryGetValueFromObjectAs("text", out string strResult));
            Assert.Equal("text", strResult);

            // Int + Numerical conversion fail
            Assert.True(dataConverter.TryGetValueFromObjectAs("152", out int intResult));
            Assert.Equal(152, intResult);
            Assert.False(dataConverter.TryGetValueFromObjectAs("1662A", out intResult));

            // Double
            Assert.True(dataConverter.TryGetValueFromObjectAs("1444.73998", out double doubleResult));
            Assert.Equal(1444.73998d, doubleResult);

            // Bool
            Assert.True(dataConverter.TryGetValueFromObjectAs("TRUE", out bool boolResult));
            Assert.True(boolResult);
            Assert.True(dataConverter.TryGetValueFromObjectAs("false", out boolResult));
            Assert.False(boolResult);
            Assert.True(dataConverter.TryGetValueFromObjectAs("True", out boolResult));
            Assert.True(boolResult);
            Assert.False(dataConverter.TryGetValueFromObjectAs("1", out boolResult));

            // Enum
            Assert.True(dataConverter.TryGetValueFromObjectAs("VALUE_FOUR", out TestEnum enumResult));
            Assert.Equal(TestEnum.VALUE_FOUR, enumResult);
            Assert.True(dataConverter.TryGetValueFromObjectAs("value_one", out enumResult));
            Assert.Equal(TestEnum.VALUE_ONE, enumResult);
        }

        [Theory]
        [ClassData(typeof(TestDataProvider))]
        public void StringTest(NameValueMapHelper mapHelper, Dictionary<string, object> expectedResults)
        {
            string str1 = mapHelper.AsString("StringValue");
            Assert.Equal(expectedResults["StringValue"], str1);
        }

        [Theory]
        [ClassData(typeof(TestDataProvider))]
        public void StringCollectionTest(NameValueMapHelper mapHelper, Dictionary<string, object> expectedResults)
        {
            IEnumerable<string> strCollection1 = mapHelper.AsStringCollection("StringCollection");
            Assert.Equal(expectedResults["StringCollection"], strCollection1);
        }

        [Theory]
        [ClassData(typeof(TestDataProvider))]
        public void IntTest(NameValueMapHelper mapHelper, Dictionary<string, object> expectedResults)
        {
            int intValue = mapHelper.AsInt("IntValue");
            Assert.Equal(expectedResults["IntValue"], intValue);
        }

        [Theory]
        [ClassData(typeof(TestDataProvider))]
        public void IntCollectionTest(NameValueMapHelper mapHelper, Dictionary<string, object> expectedResults)
        {
            IEnumerable<int> intCollection = mapHelper.AsIntCollection("IntCollection");
            Assert.Equal(expectedResults["IntCollection"], intCollection);
        }

        [Theory]
        [ClassData(typeof(TestDataProvider))]
        public void DoubleTest(NameValueMapHelper mapHelper, Dictionary<string, object> expectedResults)
        {
            double doubleValue = mapHelper.AsDouble("DoubleValue");
            Assert.Equal(expectedResults["DoubleValue"], doubleValue);
        }

        [Theory]
        [ClassData(typeof(TestDataProvider))]
        public void DoubleCollectionTest(NameValueMapHelper mapHelper, Dictionary<string, object> expectedResults)
        {
            IEnumerable<double> doubleCollection = mapHelper.AsDoubleCollection("DoubleCollection");
            Assert.Equal(expectedResults["DoubleCollection"], doubleCollection);
        }

        [Theory]
        [ClassData(typeof(TestDataProvider))]
        public void BoolTest(NameValueMapHelper mapHelper, Dictionary<string, object> expectedResults)
        {
            bool boolValue = mapHelper.AsBool("BoolValue");
            Assert.Equal(expectedResults["BoolValue"], boolValue);
        }

        [Theory]
        [ClassData(typeof(TestDataProvider))]
        public void BoolCollectionTest(NameValueMapHelper mapHelper, Dictionary<string, object> expectedResults)
        {
            IEnumerable<bool> doubleCollection = mapHelper.AsBoolCollection("BoolCollection");
            Assert.Equal(expectedResults["BoolCollection"], doubleCollection);
        }

        [Theory]
        [ClassData(typeof(TestDataProvider))]
        public void EnumTest(NameValueMapHelper mapHelper, Dictionary<string, object> expectedResults)
        {
            TestEnum enumValue = mapHelper.AsEnum<TestEnum>("EnumValue");
            Assert.Equal(expectedResults["EnumValue"], enumValue);
        }

        [Theory]
        [ClassData(typeof(TestDataProvider))]
        public void Enum1CollectionTest(NameValueMapHelper mapHelper, Dictionary<string, object> expectedResults)
        {
            IEnumerable<TestEnum> enumCollection = mapHelper.AsEnumCollection<TestEnum>("EnumCollection");
            Assert.Equal(expectedResults["EnumCollection"], enumCollection);
        }
    }
}
