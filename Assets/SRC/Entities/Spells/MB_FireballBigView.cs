using UnityEngine;
using Models;

namespace Entities.Spells
{

    public interface IFireballView:IInteractable
    {
        Vector3 Position { get; set; }
    }

    public class MB_FireballBigView : MonoBehaviour, IFireballView
    {
        public ENTITY_TYPE EntityType => _presenter.Type; 
        public uint EntityId => _presenter.Id;
        public InteractController interactController { get; set; }
        public Vector3 Position 
        {
            get => transform.position;
            set => transform.position = value;
        }

        private FireballBigPres _presenter;

        private void Start() {
            _presenter = new FireballBigPres(this);
        }
    }


    public class FireballBigPres : BaseEntity
    {

        private IFireballView _view;
        private CharacterModel _character;

        public FireballBigPres(IFireballView view):base(ENTITY_TYPE.SPELL_FIREBALL_BIG)
        {
            _character = CharacterModel.Create(Type, Id);

            _view = view;
            _character.Position = view.Position;
        }

        public override void OnUpdate(float dt)
        {

        }

        public override void OnDelete()
        {
        }
    }
}