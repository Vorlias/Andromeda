using Andromeda2D.Entities;
using Andromeda2D.Entities.Components;
using Andromeda2D.System;
using Andromeda2D.System.Internal;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Andromeda.Debugging
{

    /// <summary>
    /// Formatted console debugging
    /// </summary>
    public static class Debug
    {

        private static bool? _consoleActive;
        private static TextWriter _fStream;

        internal static TextWriter DebugStream
        {
            get
            {
                if (_fStream == null)
                    _fStream = File.CreateText("debug.log");

                return _fStream;
            }
        }

        internal static void LogFile(string contents, params object[] arg)
        {
            var dateTime = DateTime.Now;
            
            DebugStream.WriteLine(String.Format("{0:MM/dd/yyyy h:m:s tt}: ",dateTime) + contents, arg);
            DebugStream.Flush();
        }

        public static bool IsConsoleActive
        {
            get
            {
                if (_consoleActive == null)
                {
                    _consoleActive = true;
                    try
                    {
                        int windowHeight = Console.WindowHeight;
                    }
                    catch
                    {
                        _consoleActive = false;
                    }
                }
                return _consoleActive.Value;
            }
        }

        public static bool Enabled
        {
            get;
            set;
        }

        private static void AddInstanceReference(object instance)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("{0} ", instance.ToString());
            Console.ForegroundColor = oldColor;
        }

        private static void AddWarningTag(string errorMessage, params object[] arg)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[WARNING] ");
            Console.WriteLine(errorMessage, arg);
            Console.ForegroundColor = oldColor;
        }

        internal static void WriteErrorLine(string errorMessage, params object[] arg)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[PROBLEM] ");
            Console.WriteLine(errorMessage, arg);
            Console.ForegroundColor = oldColor;
        }

        internal static void WriteStackTraceLine(string stackTrace, params object[] arg)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t" + stackTrace, arg);
            Console.ForegroundColor = oldColor;
        }

        internal static void PauseExit()
        {
            Console.ReadKey();
            Environment.Exit(-1);
        }

        private static void AddInfoTag()
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[INFO] ");
            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Writes a line to the console - similar to Console.WriteLine
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="arg">Any objects to use with the message string</param>
        public static void Log(string message, params object[] arg)
        {
            if (Enabled)
            {
                AddInfoTag();
                Console.WriteLine(message, arg);
                LogFile("[INFO] " + message, arg);
            }
        }

        public static void Log(Application app, string message = "", params object[] arg)
        {
            LogInstance("Application:" + app.Title, message, arg);
        }

        public static void Log(GameState state, string message = "", params object[] arg)
        {
            LogInstance("State:" + state.Name, message, arg);
        }

        public static void Log(IGameView view, string message = "", params object[] arg)
        {
            LogInstance("View:" + view.GetType().Name, message, arg);
        }

        public static void Log(Entity entity, string message = "", params object[] arg)
        {
            LogInstance(entity.FullName, message, arg);
        }

        public static void Log(IComponent component, string message = "", params object[] arg)
        {
            LogInstance(component.Entity.FullName + ":" + component.GetType().Name, message, arg);
        }

        public static void Warn(string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            if (Enabled)
            {
                AddWarningTag(message);
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\tSource {0}, {1}:{2}", memberName, filePath, callerLineNumber);
                Console.ForegroundColor = oldColor;

                LogFile("[WARNING] " + message);
            }
        }

        public static void Error(string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            if (Enabled)
            {
                WriteErrorLine(message);
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\tSource: {0}, {1}:{2}", memberName, filePath, callerLineNumber);
                Console.ForegroundColor = oldColor;

                LogFile("[PROBLEM] " + message);
                LogFile("\tSource: {0}, {1}:{2}", memberName, filePath, callerLineNumber);
                PauseExit();
            }
        }

        /// <summary>
        /// Writes the instance to the output
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="message">A message to go with the instance</param>
        /// <param name="arg">Any arguments for the formatted message string</param>
        public static void LogInstance(object instance, string message = "", params object[] arg)
        {
            if (Enabled)
            {
                AddInfoTag();
                AddInstanceReference(instance);
                Console.WriteLine(message, arg);
                LogFile("[INFO] <" + instance.ToString() + "> " + message, arg);
            }
        }
    }
}
