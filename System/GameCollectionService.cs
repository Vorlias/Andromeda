using System.Collections.Generic;

namespace Vorlias2D.System
{
    public abstract class GameCollectionService<ManageableType>
    {
        protected Dictionary<string, ManageableType> collection;

        public StateGameManager GameManager
        {
            get;
        }

        public bool Has(string name)
        {
            return collection.ContainsKey(name);
        }

        internal GameCollectionService(StateGameManager parent)
        {
            collection = new Dictionary<string, ManageableType>();
            GameManager = parent;
        }
    }
}
