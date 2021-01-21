//
// Log.cs
//
// Author:
//   Aaron Bockover <abockover@novell.com>
//   Gir Core Developers
//
// Copyright (C) 2005-2007 Novell, Inc.
// Copyright (C) 2020 Gir Core Developers.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace Repository
{
    public delegate void LogNotifyHandler (LogNotifyArgs args);

    public class LogNotifyArgs : EventArgs
    {
        private LogEntry entry;

        public LogNotifyArgs (LogEntry entry)
        {
            this.entry = entry;
        }

        public LogEntry Entry {
            get { return entry; }
        }
    }

    public enum LogEntryType
    {
        Debug,
        Warning,
        Error,
        Information
    }

    public class LogEntry
    {
        private LogEntryType type;
        private string message;
        private string details;
        private DateTime timestamp;

        internal LogEntry (LogEntryType type, string message, string details)
        {
            this.type = type;
            this.message = message;
            this.details = details;
            this.timestamp = DateTime.Now;
        }

        public LogEntryType Type {
            get { return type; }
        }

        public string Message {
            get { return message; }
        }

        public string Details {
            get { return details; }
        }

        public DateTime TimeStamp {
            get { return timestamp; }
        }
    }

    public static class Log
    {
        public static event LogNotifyHandler Notify;

        private static Dictionary<uint, DateTime> timers = new Dictionary<uint, DateTime> ();
        private static uint next_timer_id = 1;

        private static bool debugging = false;
        public static bool Debugging {
            get { return debugging; }
            set { debugging = value; }
        }

        public static void Commit (LogEntryType type, string message, string details, bool showUser)
        {
            if (type == LogEntryType.Debug && !Debugging) {
                return;
            }

            if (type != LogEntryType.Information || (type == LogEntryType.Information && !showUser)) {
                switch (type) {
                    case LogEntryType.Error: Console.ForegroundColor = ConsoleColor.Red; break;
                    case LogEntryType.Warning: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                    case LogEntryType.Information: Console.ForegroundColor = ConsoleColor.Green; break;
                    case LogEntryType.Debug: Console.ForegroundColor = ConsoleColor.Blue; break;
                }

                var thread_name = String.Empty;
                if (Debugging) {
                    var thread = Thread.CurrentThread;
                    thread_name = String.Format ("{0} ", thread.ManagedThreadId);
                }

                Console.Write ("[{5}{0} {1:00}:{2:00}:{3:00}.{4:000}]", TypeString (type), DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, thread_name);

                Console.ResetColor ();

                if (details != null) {
                    Console.WriteLine (" {0} - {1}", message, details);
                } else {
                    Console.WriteLine (" {0}", message);
                }
            }

            if (showUser) {
                OnNotify (new LogEntry (type, message, details));
            }
        }

        private static string TypeString (LogEntryType type)
        {
            switch (type) {
                case LogEntryType.Debug:         return "Debug";
                case LogEntryType.Warning:       return "Warn ";
                case LogEntryType.Error:         return "Error";
                case LogEntryType.Information:   return "Info ";
            }
            return null;
        }

        private static void OnNotify (LogEntry entry)
        {
            LogNotifyHandler handler = Notify;
            if (handler != null) {
                handler (new LogNotifyArgs (entry));
            }
        }

        #region Timer Methods

        public static uint DebugTimerStart (string message)
        {
            return TimerStart (message, false);
        }

        public static uint InformationTimerStart (string message)
        {
            return TimerStart (message, true);
        }

        private static uint TimerStart (string message, bool isInfo)
        {
            if (!Debugging && !isInfo) {
                return 0;
            }

            if (isInfo) {
                Information (message);
            } else {
                Debug (message);
            }

            return TimerStart (isInfo);
        }

        public static uint DebugTimerStart ()
        {
            return TimerStart (false);
        }

        public static uint InformationTimerStart ()
        {
            return TimerStart (true);
        }

        private static uint TimerStart (bool isInfo)
        {
            if (!Debugging && !isInfo) {
                return 0;
            }

            uint timer_id = next_timer_id++;
            timers.Add (timer_id, DateTime.Now);
            return timer_id;
        }

        public static void DebugTimerPrint (uint id)
        {
            if (!Debugging) {
                return;
            }

            TimerPrint (id, "Operation duration: {0}", false);
        }

        public static void DebugTimerPrint (uint id, string message)
        {
            if (!Debugging) {
                return;
            }

            TimerPrint (id, message, false);
        }

        public static void InformationTimerPrint (uint id)
        {
            TimerPrint (id, "Operation duration: {0}", true);
        }

        public static void InformationTimerPrint (uint id, string message)
        {
            TimerPrint (id, message, true);
        }

        private static void TimerPrint (uint id, string message, bool isInfo)
        {
            if (!Debugging && !isInfo) {
                return;
            }

            DateTime finish = DateTime.Now;

            if (!timers.ContainsKey (id)) {
                return;
            }

            TimeSpan duration = finish - timers[id];
            string d_message;
            if (duration.TotalSeconds < 60) {
                d_message = duration.TotalSeconds.ToString ();
            } else {
                d_message = duration.ToString ();
            }

            if (isInfo) {
                InformationFormat (message, d_message);
            } else {
                DebugFormat (message, d_message);
            }
        }

        #endregion

        #region Public Debug Methods

        public static void Debug (string message, string details)
        {
            if (Debugging) {
                Commit (LogEntryType.Debug, message, details, false);
            }
        }

        public static void Debug (string message)
        {
            if (Debugging) {
                Debug (message, null);
            }
        }

        public static void DebugFormat (string format, params object [] args)
        {
            if (Debugging) {
                Debug (String.Format (format, args));
            }
        }

        #endregion

        #region Public Information Methods

        public static void Information (string message)
        {
            Information (message, null);
        }

        public static void Information (string message, string details)
        {
            Information (message, details, false);
        }

        public static void Information (string message, string details, bool showUser)
        {
            Commit (LogEntryType.Information, message, details, showUser);
        }

        public static void Information (string message, bool showUser)
        {
            Information (message, null, showUser);
        }

        public static void InformationFormat (string format, params object [] args)
        {
            Information (String.Format (format, args));
        }

        #endregion

        #region Public Warning Methods

        public static void Warning (string message)
        {
            Warning (message, null);
        }

        public static void Warning (string message, string details)
        {
            Warning (message, details, false);
        }

        public static void Warning (string message, string details, bool showUser)
        {
            Commit (LogEntryType.Warning, message, details, showUser);
        }

        public static void Warning (string message, bool showUser)
        {
            Warning (message, null, showUser);
        }

        public static void WarningFormat (string format, params object [] args)
        {
            Warning (String.Format (format, args));
        }

        #endregion

        #region Public Error Methods

        public static void Error (string message)
        {
            Error (message, null);
        }

        public static void Error (string message, string details)
        {
            Error (message, details, false);
        }

        public static void Error (string message, string details, bool showUser)
        {
            Commit (LogEntryType.Error, message, details, showUser);
        }

        public static void Error (string message, bool showUser)
        {
            Error (message, null, showUser);
        }

        public static void ErrorFormat (string format, params object [] args)
        {
            Error (String.Format (format, args));
        }

        #endregion

        #region Public Exception Methods

        public static void DebugException (Exception e)
        {
            if (Debugging) {
                Exception (e);
            }
        }

        public static void Exception (Exception e)
        {
            Exception (null, e);
        }

        public static void Exception (string message, Exception e)
        {
            Stack<Exception> exception_chain = new Stack<Exception> ();
            StringBuilder builder = new StringBuilder ();

            while (e != null) {
                exception_chain.Push (e);
                e = e.InnerException;
            }

            while (exception_chain.Count > 0) {
                e = exception_chain.Pop ();
                builder.AppendFormat ("{0}: {1} (in `{2}`)", e.GetType (), e.Message, e.Source).AppendLine ();
                builder.Append (e.StackTrace);
                if (exception_chain.Count > 0) {
                    builder.AppendLine ();
                }
            }

            Log.Warning (message ?? "Caught an exception", builder.ToString (), false);
        }

        #endregion
    }
}
