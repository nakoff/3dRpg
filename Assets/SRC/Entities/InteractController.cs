using Models;
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
            var interactView = col.gameObject.GetComponent<IInteractable>();
            OnCollision(interactView);
        }

        public void OnCollisionEnter(Collider col)
        {
            var interactView = col.gameObject.GetComponent<IInteractable>();
            OnCollision(interactView);
        }

        private void OnCollision(IInteractable targetView)
        {
            if (targetView == null)
                return;

            var target = new InteractStateModel.TargetEntity(targetView.EntityType, targetView.EntityId);
            _interactState._InteractedWith(target);
        }

    }
}