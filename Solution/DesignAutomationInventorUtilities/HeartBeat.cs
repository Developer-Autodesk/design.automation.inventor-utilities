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
using System.Threading;

namespace Autodesk.Forge.DesignAutomation.Inventor.Utils
{
    public class HeartBeat : IDisposable
    {
        // default is 50s
        public HeartBeat(int intervalMillisec = 50000)
        {

            t = new Thread(() =>
            {

                LogTrace("HeartBeating every {0}ms.", intervalMillisec);

                for (; ; )
                {
                    if (mre.WaitOne(TimeSpan.FromMilliseconds(intervalMillisec)))
                    {
                        break;
                    }
                    LogTrace("HeartBeat {0}.", (long)(new TimeSpan(DateTime.Now.Ticks - ticks).TotalSeconds));
                }

            });

            ticks = DateTime.Now.Ticks;
            t.Start();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (t != null)
                {
                    LogTrace("Ending HeartBeat");
                    mre.Set();
                    t.Join();
                    t = null;
                }
            }
        }

        /// <summary>
        /// Log message with 'trace' log level.
        /// </summary>
        private void LogTrace(string format, params object[] args)
        {
            Trace.TraceInformation(format, args);
        }

        private Thread t;
        private ManualResetEvent mre = new ManualResetEvent(false);
        private long ticks;
    }
}
