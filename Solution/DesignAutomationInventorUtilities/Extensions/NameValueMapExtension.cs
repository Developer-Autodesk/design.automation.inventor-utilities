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
using System.Linq;

namespace Autodesk.Forge.DesignAutomation.Inventor.Utils.Helpers
{
    public class InvalidValueTypeException : Exception
    {
        public InvalidValueTypeException(string message) : base(message) { }
    }

    public static class NameValueMapExtension
    {
        private static readonly DataConverter dataConverter = new DataConverter();
        private static readonly char[] Separators = " ,".ToCharArray();

        public static bool HasKey(this NameValueMap nameValueMap, string key)
        {
            bool hasKey;

            try
            {
                hasKey = nameValueMap.Value[key] != null;
            }
            catch (Exception) { hasKey = false; }

            return hasKey;
        }

        public static string AsString(this NameValueMap nameValueMap, string index)
        {
            if (nameValueMap.TryGetValueAs(index, out string strValue))
                return strValue;

            throw new InvalidValueTypeException("Value cannot be used as a string");
        }

        public static int AsInt(this NameValueMap nameValueMap, string index)
        {
            if (nameValueMap.TryGetValueAs(index, out int intValue))
                return intValue;

            throw new InvalidValueTypeException("Value cannot be used as an integer");
        }

        public static double AsDouble(this NameValueMap nameValueMap, string index)
        {
            if (nameValueMap.TryGetValueAs(index, out double doubleValue))
                return doubleValue;

            throw new InvalidValueTypeException("Value cannot be used as a double");
        }

        public static bool AsBool(this NameValueMap nameValueMap, string index)
        {
            if (nameValueMap.TryGetValueAs(index, out bool boolValue))
                return boolValue;

            throw new InvalidValueTypeException("Value cannot be used as a boolean");
        }

        public static T AsEnum<T>(this NameValueMap nameValueMap, string index)
        {
            if (nameValueMap.TryGetValueAs(index, out T enumValue))
                return enumValue;

            throw new InvalidValueTypeException("Value cannot be used as an enum");
        }

        public static IEnumerable<string> AsStringCollection(this NameValueMap nameValueMap, string index)
        {
            return nameValueMap.GetValueAsCollection<string>(index);
        }

        public static IEnumerable<int> AsIntCollection(this NameValueMap nameValueMap, string index)
        {
            return nameValueMap.GetValueAsCollection<int>(index);
        }

        public static IEnumerable<double> AsDoubleCollection(this NameValueMap nameValueMap, string index)
        {
            return nameValueMap.GetValueAsCollection<double>(index);
        }

        public static IEnumerable<bool> AsBoolCollection(this NameValueMap nameValueMap, string index)
        {
            return nameValueMap.GetValueAsCollection<bool>(index);
        }

        public static IEnumerable<T> AsEnumCollection<T>(this NameValueMap nameValueMap, string index)
        {
            return nameValueMap.GetValueAsCollection<T>(index);
        }

        public static IEnumerable<T> GetValueAsCollection<T>(this NameValueMap nameValueMap, string index)
        {
            if (!nameValueMap.TryGetValueAs(index, out string outString))
                throw new InvalidValueTypeException("Value cannot be used as a collection because it is not a string");

            return outString
                .Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(item =>
                {
                    if (!dataConverter.TryGetValueFromObjectAs(item, out T outValue))
                        throw new InvalidValueTypeException("Value cannot be used as a collection");

                    return outValue;
                })
                .ToList();
        }

        public static bool TryGetValueAs<T>(this NameValueMap nameValueMap, string index, out T outValue)
        {
            object value;

            try
            {
                value = nameValueMap.Value[index];
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch(Exception)
            {
                outValue = default; return false; 
            }

            return dataConverter.TryGetValueFromObjectAs(value, out outValue);
        }
    }
}
