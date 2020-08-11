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

using Inventor;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Autodesk.Forge.DesignAutomation.Inventor.Utils.Helpers
{
    public class InvalidValueTypeException : Exception
    {
        public InvalidValueTypeException(string message) : base(message) {}
    }

    public class NameValueMapHelper
    {
        private readonly NameValueMap nameValueMap;

        public NameValueMapHelper(NameValueMap nameValueMap)
        {
            this.nameValueMap = nameValueMap;
        }

        public bool HasKey(string key) => nameValueMap.Value[key] != null;

        public string AsString(string index)
        {
            if (TryGetValueAs(index, out string strValue))
                return strValue;

            throw new InvalidValueTypeException("Value cannot be used as a string");
        }

        public int AsInt(string index)
        {
            if (TryGetValueAs(index, out int intValue))
                return intValue;

            throw new InvalidValueTypeException("Value cannot be used as an integer");
        }

        public double AsDouble(string index)
        {
            if (TryGetValueAs(index, out double doubleValue))
                return doubleValue;

            throw new InvalidValueTypeException("Value cannot be used as a double");
        }

        public bool AsBool(string index)
        {
            if (TryGetValueAs(index, out bool boolValue))
                return boolValue;

            throw new InvalidValueTypeException("Value cannot be used as a boolean");
        }

        public T AsBool<T>(string index)
        {
            if (TryGetValueAs(index, out T enumValue))
                return enumValue;

            throw new InvalidValueTypeException("Value cannot be used as an enum");
        }

        public IEnumerable<string> AsStringCollection(string index)
        {
            return GetValuesCollection<string>(index);
        }

        public IEnumerable<int> AsIntCollection(string index)
        {
            return GetValuesCollection<int>(index);
        }

        public IEnumerable<double> AsDoubleCollection(string index)
        {
            return GetValuesCollection<double>(index);
        }

        public IEnumerable<bool> AsBoolCollection(string index)
        {
            return GetValuesCollection<bool>(index);
        }

        public IEnumerable<T> AsEnumCollection<T>(string index)
        {
            return GetValuesCollection<T>(index);
        }

        public IEnumerable<T> GetValuesCollection<T>(string index)
        {
            if (!TryGetValueAs(index, out string outString))
                throw new InvalidValueTypeException("Value cannot be used as a collection because it is not a string");

            string[] splitValue = outString.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            List<T> list = new List<T>();
            foreach (string subValue in splitValue) 
            {
                if (!TryGetValueFromObjectAs(subValue, out T outValue))
                    throw new InvalidValueTypeException("Value cannot be used as a collection");

                list.Add(outValue);
            }

            return list;
        }

        public bool TryGetValueAs<T>(string index, out T outValue)
        {
            var value = nameValueMap.Value[index];
            return TryGetValueFromObjectAs<T>(value, out outValue);
        }

        private bool TryGetValueFromObjectAs<T>(object value, out T outValue)
        {
            if (typeof(T).IsEnum) 
            {
                try
                {
                    outValue = (T)Enum.Parse(typeof(T), value.ToString(), true);
                }
                catch (Exception)
                {
                    throw new InvalidValueTypeException("Value cannot be used as an enum");
                }

                return true;
            }

            if (value is T convertedValue)
            {
                outValue = convertedValue;
                return true;
            }

            bool success;

            try
            {
                outValue = (T)Convert.ChangeType(value, typeof(T));
                success = true;
            }
            catch (Exception)
            {
                outValue = default(T);
                success = false;
            }

            return success;
        }
    }
}
