namespace Andromeda2D.Entities.Components.Internal
{
    /// <summary>
    /// The priority of updates in the game update queue
    /// </summary>
    public enum UpdatePriority
    {
        

        /// <summary>
        /// Updates before everything else
        /// </summary>
        First = 0,

        /// <summary>
        /// Updates when the network does (if using Vorlias2D.Multiplayer)
        /// </summary>
        Network = 50,

        /// <summary>
        /// Updates when physics does
        /// </summary>
        Physics = 100,

        /// <summary>
        /// Updates when the camera does
        /// </summary>
        Camera = 150,

        

        /// <summary>
        /// Updates when the interfaces do
        /// </summary>
        Interface = 200,

        /// <summary>
        /// Updates at normal
        /// </summary>
        Normal = 250,

        /// <summary>
        /// Updates last, after everything else.
        /// </summary>
        Last = 300,
    }
}
