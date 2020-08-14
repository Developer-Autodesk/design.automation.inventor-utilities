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

    public class NameValueMapHelper : NameValueMap
    {
        private readonly NameValueMap nameValueMap;
        private readonly DataConverter dataConverter;

        public NameValueMapHelper(NameValueMap nameValueMap)
        {
            this.nameValueMap = nameValueMap;
            dataConverter = new DataConverter();
        }

        public bool HasKey(string key)
        {
            bool hasKey;

            try
            {
                hasKey = nameValueMap.Value[key] != null;
            }
            catch (Exception) { hasKey = false; }

            return hasKey;
        }

        public string AsString(string index)
        {
            if (TryGetValueAs(index, out string strValue))
                return strValue;

            ThrowException(index, "Value cannot be used as a string");
            return default;
        }

        public int AsInt(string index)
        {
            if (TryGetValueAs(index, out int intValue))
                return intValue;

            ThrowException(index, "Value cannot be used as an integer");
            return default;
        }

        public double AsDouble(string index)
        {
            if (TryGetValueAs(index, out double doubleValue))
                return doubleValue;

            ThrowException(index, "Value cannot be used as a double");
            return default;
        }

        public bool AsBool(string index)
        {
            if (TryGetValueAs(index, out bool boolValue))
                return boolValue;

            ThrowException(index, "Value cannot be used as a boolean");
            return default;
        }

        public T AsEnum<T>(string index)
        {
            if (TryGetValueAs(index, out T enumValue))
                return enumValue;

            ThrowException(index, "Value cannot be used as an enum");
            return default;
        }

        public IEnumerable<string> AsStringCollection(string index)
        {
            return GetValueAsCollection<string>(index);
        }

        public IEnumerable<int> AsIntCollection(string index)
        {
            return GetValueAsCollection<int>(index);
        }

        public IEnumerable<double> AsDoubleCollection(string index)
        {
            return GetValueAsCollection<double>(index);
        }

        public IEnumerable<bool> AsBoolCollection(string index)
        {
            return GetValueAsCollection<bool>(index);
        }

        public IEnumerable<T> AsEnumCollection<T>(string index)
        {
            return GetValueAsCollection<T>(index);
        }

        public IEnumerable<T> GetValueAsCollection<T>(string index)
        {
            if (!TryGetValueAs(index, out string outString))
                ThrowException(index, "Value cannot be used as a collection because it is not a string");

            string[] splitValue = outString.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            List<T> list = new List<T>();
            foreach (string subValue in splitValue) 
            {
                if (!dataConverter.TryGetValueFromObjectAs(subValue, out T outValue))
                    throw new InvalidValueTypeException("Value cannot be used as a collection");

                list.Add(outValue);
            }

            return list;
        }

        public bool TryGetValueAs<T>(string index, out T outValue)
        {
            object value;

            try
            {
                value = nameValueMap.Value[index];
            }
            catch (Exception) { outValue = default; return false; }

            return dataConverter.TryGetValueFromObjectAs<T>(value, out outValue);
        }

        private void ThrowException(string index, string errorMessage)
        {
            if (!HasKey(index))
                throw new KeyNotFoundException($"Key {index} was not found inside of the map");
            throw new InvalidValueTypeException(errorMessage);
        }

        public void Add(string Name, object Value) => nameValueMap.Add(Name, Value);

        public IEnumerator GetEnumerator() => nameValueMap.GetEnumerator();

        public void Clear() => nameValueMap.Clear();

        public void Remove(object Index) => nameValueMap.Remove(Index);

        public void Insert(string Name, object Value, object TargetIndex, bool InsertBefore = true)
            => nameValueMap.Insert(Name, Value, TargetIndex, InsertBefore);

        public object get_Value(string Name) => nameValueMap.Value[Name];

        public void set_Value(string Name, object value) => nameValueMap.Value[Name] = value;

        public int Count => nameValueMap.Count;

        public object get_Item(object Index) => nameValueMap.Item[Index];

        public string get_Name(int Index) => nameValueMap.Name[Index];
    }
}
