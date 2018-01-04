using Andromeda.System;
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
        /// The text value of this TextInput
        /// </summary>
        string Value
        {
            get;
            set;
        }

        bool Focused
        {
            get;
            set;
        }

        /// <summary>
        /// The manager (if it is using this object)
        /// </summary>
        UserInputManager Manager
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the TextInput supports multiline
        /// </summary>
        bool IsMultiline
        {
            get;
        }
    }
}
