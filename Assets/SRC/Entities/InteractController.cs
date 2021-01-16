using Datas;
using UnityEngine;

namespace Entities
{

    public class InteractController
    {

        private InteractStateModel _interactState;

        public InteractController(InteractStateObject obj)
        {
            _interactState = new InteractStateModel(obj);
        }

        public void OnCollisionEnter(Collision col)
        {
            if (col is IInteractable interactView)
            {
                var collired = new InteractStateModel.TargetEntity(interactView.EntityType, interactView.EntityId);
                _interactState._InteractedWith(collired);
            }
        }

    }
}