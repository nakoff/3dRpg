using UnityEngine;

namespace Entities.Freezer
{

    public interface IFreezerView : IInteractable
    {
        Vector3 Position { get; set; }
    }


    [RequireComponent (typeof(Rigidbody))]
    public class MB_FreezerView : MonoBehaviour, IFreezerView
    {
        public ENTITY_TYPE EntityType => _presenter.Type; 
        public uint EntityId => _presenter.Id;
        public InteractController interactController { get; set; }

        public Vector3 Position 
        {
            get => gameObject.transform.position;
            set => gameObject.transform.position = value;
        }

        private FreezerPres _presenter;


        private void Start() {
            _presenter = new FreezerPres(this);            
        }

        private void OnTriggerEnter(Collider other) {
            interactController.OnCollisionEnter(other);
        }

    }
}