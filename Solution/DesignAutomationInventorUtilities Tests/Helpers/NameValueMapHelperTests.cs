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
        VALUE_ONE,
        VALUE_TWO,
        VALUE_THREE,
        VALUE_FOUR,
        VALUE_FIVE
    }

    public class NameValueMapHelperTests
    {
        private NameValueMap nameValueMap;

        public NameValueMapHelperTests()
        {
            nameValueMap = new NameValueMapStub();
        }

        [Fact]
        public void ExceptionsTest()
        {
            NameValueMap nameValueMap = new NameValueMapStub();
            nameValueMap.Value["StringValue"] = "Test String";

            Assert.Throws<KeyNotFoundException>(() => nameValueMap.AsString("WrongIndex"));
            Assert.Throws<InvalidValueTypeException>(() => nameValueMap.AsInt("StringValue"));
            Assert.Throws<KeyNotFoundException>(() => nameValueMap.AsStringCollection("WrongIndex"));
            Assert.Throws<InvalidValueTypeException>(() => nameValueMap.AsIntCollection("StringValue"));
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

            Assert.True(nameValueMap.HasKey("StringValue"));
            Assert.True(nameValueMap.HasKey("IntValue"));
            Assert.True(nameValueMap.HasKey("DoubleValue"));
            Assert.True(nameValueMap.HasKey("BoolValue"));
            Assert.False(nameValueMap.HasKey("NonExistantKey"));
            Assert.True(nameValueMap.HasKey("EnumValue"));
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

        [Fact]
        public void StringTest()
        {
            var expectedResult = "TestString";
            nameValueMap.Value["StringValue"] = "TestString";
            

            string str1 = nameValueMap.AsString("StringValue");
            Assert.Equal(expectedResult, str1);
        }

        [Fact]
        public void StringCollectionTest()
        {
            var expectedResult = new List<string> { "Alpha", "beta", "gamma", "1", "2", "3", "4", "delta", "6", "longer_teeeeeextttttt" };
            nameValueMap.Value["StringCollection"] = "Alpha, beta, gamma, 1, 2, 3, 4, delta, 6, longer_teeeeeextttttt";
            

            IEnumerable<string> strCollection1 = nameValueMap.AsStringCollection("StringCollection");
            Assert.Equal(expectedResult, strCollection1);
        }

        [Fact]
        public void IntTest()
        {
            var expectedResult = 356;
            nameValueMap.Value["IntValue"] = "356";
           

            int intValue = nameValueMap.AsInt("IntValue");
            Assert.Equal(expectedResult, intValue);
        }

        [Fact]
        public void IntCollectionTest()
        {
            var expectedResult = new List<int> { 5, 8, 9, 100, 2, 3, 4096, 5, 60 };
            nameValueMap.Value["IntCollection"] = "5, 8, 9, 100, 2, 3, 4096, 5, 60";

            IEnumerable<int> intCollection = nameValueMap.AsIntCollection("IntCollection");
            Assert.Equal(expectedResult, intCollection);
        }

        [Fact]
        public void DoubleTest()
        {
            var expectedResult = 114.3998d;
            nameValueMap.Value["DoubleValue"] = "114.3998";

            double doubleValue = nameValueMap.AsDouble("DoubleValue");
            Assert.Equal(expectedResult, doubleValue);
        }

        [Fact]
        public void DoubleCollectionTest()
        {
            var expectedResult = new List<double> { 4.2d, 11.82d, 9.156d, 10009.45d, 200.42d, 30.333d, 4.2d, 12.0d, 9.0d };
            nameValueMap.Value["DoubleCollection"] = "4.2, 11.82, 9.156, 10009.45, 200.42, 30.333, 4.2, 12.0, 9.0";

            IEnumerable<double> doubleCollection = nameValueMap.AsDoubleCollection("DoubleCollection");
            Assert.Equal(expectedResult, doubleCollection);
        }

        [Fact]
        public void BoolTest()
        {
            var expectedResult = true;
            nameValueMap.Value["BoolValue"] = "True";

            bool boolValue = nameValueMap.AsBool("BoolValue");
            Assert.Equal(expectedResult, boolValue);
        }

        [Fact]
        public void BoolCollectionTest()
        {
            var expectedResult = new List<bool> { true, false, false, true, false, true, false, true, true };
            nameValueMap.Value["BoolCollection"] = "True, False, FALSE, TRUE, False, true, false, true, True";

            IEnumerable<bool> doubleCollection = nameValueMap.AsBoolCollection("BoolCollection");
            Assert.Equal(expectedResult, doubleCollection);
        }

        [Fact]
        public void EnumTest()
        {
            var expectedResult = TestEnum.VALUE_FOUR;
            nameValueMap.Value["EnumValue"] = "VALUE_FOUR";

            TestEnum enumValue = nameValueMap.AsEnum<TestEnum>("EnumValue");
            Assert.Equal(expectedResult, enumValue);
        }

        [Fact]
        public void EnumCollectionTest()
        {
            var expectedResult = new List<TestEnum> { TestEnum.VALUE_ONE, TestEnum.VALUE_TWO, TestEnum.VALUE_THREE, TestEnum.VALUE_FIVE, TestEnum.VALUE_FOUR };
            nameValueMap.Value["EnumCollection"] = "VALUE_ONE, VALUe_TWO, VALUE_THREE, VALUE_FIVE, VALUE_FOUR";

            IEnumerable<TestEnum> enumCollection = nameValueMap.AsEnumCollection<TestEnum>("EnumCollection");
            Assert.Equal(expectedResult, enumCollection);
        }
    }
}
