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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Autodesk.Forge.DesignAutomation.Inventor.Utils
{
    public class OnDemand
    {
        /// <summary>
        /// It downloads an onDemand argument. It is a synchronous operation.
        /// </summary>
        /// <param name="name">Argument name in activity you want to download</param>
        /// <param name="suffix">It is possible to add a suffix to URL given in a workitem</param>
        /// <param name="headers">It allows you to add HTTP headers.</param>
        /// <param name="requestContentOrFile">The Body of the HTTP request</param>
        /// <param name="responseFile">The name of the downloaded file. Use the following format file://filename.ext</param>
        /// <returns>True when download ends successfully</returns>
        public static bool HttpOperation(string name, string suffix, Dictionary<string, string> headers,
            string responseFile, string requestContentOrFile=null)
        {
            // Throw an exception for values other than empty string or null 
            if (requestContentOrFile != null && requestContentOrFile.Length > 0)
            {
                throw NotImplementedException("parameter requestContentOrFile not supported");
            }

            string headersStr = ParseHeaders(headers);

            LogTrace("!ACESAPI:acesHttpOperation({0},{1},{2},{3},{4})",
                name ?? "", suffix ?? "", headersStr ?? "", requestContentOrFile ?? "", responseFile ?? "");

            int idx = 0;
            while (true)
            {
                char ch = Convert.ToChar(Console.Read());
                if (ch == '\x3')
                {
                    return false;
                }
                else if (ch == '\n')
                {
                    return true;
                }

                if (idx >= 16)
                {
                    return false;
                }

                idx++;
            }
        }

        private static Exception NotImplementedException(string v)
        {
            throw new NotImplementedException();
        }

        private static string ParseHeaders(Dictionary<string, string> headers)
        {
            if (headers == null)
                return null;

            // Use the following format key1= value1; key2= value2
            return string.Join(";", headers.Select(x => x.Key + "=" + x.Value));
        }

        /// <summary>
        /// Log message with 'trace' log level.
        /// </summary>
        private static void LogTrace(string format, params object[] args)
        {
            Trace.TraceInformation(format, args);
        }

    }

}
