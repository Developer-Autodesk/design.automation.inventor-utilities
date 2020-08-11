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

        public T AsEnum<T>(string index, bool ignoreCase = true) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T is not an enum type");
            }

            T enumValue;

            try
            {
                enumValue = (T)Enum.Parse(typeof(T), nameValueMap.Value[index].ToString(), ignoreCase);
            }
            catch (Exception)
            {
                throw new InvalidValueTypeException("Value cannot be used as an enum");
            }

            return enumValue;
        }

        /*
        public List<NameValueMapValue> AsCollection(char separator = ' ')
        {
            List<NameValueMapValue> collection = null;

            if (value is string stringValue)
            {
                collection = new List<NameValueMapValue>();
                string[] splitString = stringValue.Split(separator);
                foreach (string str in splitString)
                    collection.Add(new NameValueMapValue(str));
            }
            else if (value is System.Collections.IEnumerable enumerableValue)
            {
                collection = new List<NameValueMapValue>();
                foreach (object value in enumerableValue)
                    collection.Add(new NameValueMapValue(value));
            }

            if (collection == null)
                throw new InvalidValueTypeException("Value cannot be used as a collection");

            return collection;
        }
        */

        public IEnumerable TryGetCollection<T>(string index)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValueAs<T>(string index, out T outValue)
        {
            var value = nameValueMap.Value[index];
            return TryGetValueAs<T>(value, out outValue);
        }

        private bool TryGetValueAs<T>(object value, out T outValue)
        {
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
