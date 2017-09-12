using Andromeda2D.System;
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
        string Text
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
