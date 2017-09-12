using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.System.Input
{
    /// <summary>
    /// An object which can accept text input
    /// </summary>
    public interface ITextInput
    {
        /// <summary>
        /// Get the current text of the input
        /// </summary>
        /// <returns>The current text of the input</returns>
        string GetText();

        /// <summary>
        /// Sets the text of the input
        /// </summary>
        /// <param name="text">The current text of the input</param>
        void SetText(string text);


        /// <summary>
        /// Whether or not the TextInput supports multiline
        /// </summary>
        bool IsMultiline
        {
            get;
        }
    }
}
