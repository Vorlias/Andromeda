namespace Andromeda.System.Input
{
    /// <summary>
    /// An object that has a text value
    /// </summary>
    public interface ITextObject
    {
        /// <summary>
        /// The text of this object
        /// </summary>
        string Text
        {
            get;
            set;
        }
    }
}
