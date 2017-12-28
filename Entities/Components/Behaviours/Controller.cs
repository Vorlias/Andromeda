using Andromeda2D.Entities;
using Andromeda2D.Entities.Components;
using Andromeda2D.Entities.Components.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Entities.Components
{

    /// <summary>
    /// A controller for a model
    /// </summary>
    /// <typeparam name="TController">The model type</typeparam>
    public abstract class Controller<TController> : Component 
        where  TController : IModel
    {
        Entity _entity;

        /// <summary>
        /// Sets the model of the controller
        /// </summary>
        /// <param name="model">The model to set to this controller</param>
        protected void SetControllerModel(TController model)
        {
            this._model = model;
        }

        private TController _model;

        /// <summary>
        /// The model of this controller
        /// </summary>
        public TController Model
        {
            get => _model;
        }

        public abstract void InitModel();

        public override void OnComponentInit(Entity entity)
        {
            InitModel();
        }
    }
}
