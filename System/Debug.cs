using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Debugging
{

    /// <summary>
    /// Formatted console debugging
    /// </summary>
    public static class Debug
    {
        private static bool? _consoleActive;
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

        private static void AddProblemTag(string errorMessage, params object[] arg)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[PROBLEM] ");
            Console.WriteLine(errorMessage, arg);
            Console.ForegroundColor = oldColor;
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
        public static void WriteLine(string message, params object[] arg)
        {
            if (Enabled)
            {
                AddInfoTag();
                Console.WriteLine(message, arg);
            }
        }

        public static void WriteError(string message, params object[] arg)
        {
            if (Enabled)
            {
                AddProblemTag(message, arg);
            }
        }

        /// <summary>
        /// Writes the instance to the output
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="message">A message to go with the instance</param>
        /// <param name="arg">Any arguments for the formatted message string</param>
        public static void WriteInstance(object instance, string message = "", params object[] arg)
        {
            if (Enabled)
            {
                AddInfoTag();
                AddInstanceReference(instance);
                Console.WriteLine(message, arg);
            }
        }
    }
}
