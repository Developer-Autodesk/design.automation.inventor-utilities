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
using System.Diagnostics;

namespace Autodesk.Forge.DesignAutomation.Inventor.Utils
{
    public class OnDemand
    {
        /// <summary>
        /// It downloads an onDemand argument. It is a synchronous operation.
        /// </summary>
        /// <param name="name">Argument name in activity you want to download</param>
        /// <param name="suffix">It is possible to add a suffix to URL given in a workitem</param>
        /// <param name="headers">It allows you to add HTTP headers. Use the following format key1:value1;key2:value2</param>
        /// <param name="requestContentOrFile">The Body of the HTTP request</param>
        /// <param name="responseFile">The name of the downloaded file. Use the following format file://filename.ext</param>
        /// <returns>True when download ends successfully</returns>
        public static bool HttpOperation(string name, string suffix, string headers, string requestContentOrFile,
            string responseFile)
        {
            LogTrace("!ACESAPI:acesHttpOperation({0},{1},{2},{3},{4})",
                name ?? "", suffix ?? "", headers ?? "", requestContentOrFile ?? "", responseFile ?? "");

            int idx = 0;
            while (true)
            {
                char ch = Convert.ToChar(Console.Read());
                if (ch == '\x3')
                {
                    return true;
                }
                else if (ch == '\n')
                {
                    return false;
                }

                if (idx >= 16)
                {
                    return false;
                }

                idx++;
            }
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
