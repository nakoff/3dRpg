using UnityEngine;
using Models;

namespace Entities.Spells
{

    public interface IFireballView:IInteractable
    {
        Vector3 Position { get; set; }
        TypeId owner { get; set; }
    }

    public class MB_FireballBigView : MonoBehaviour, IFireballView
    {
        public ENTITY_TYPE EntityType => _presenter.Type; 
        public uint EntityId => _presenter.Id;
        public InteractController interactController { get; set; }
        public TypeId owner 
        {
            get => _presenter.owner;
            set => _presenter.owner = value;
        }
        public Vector3 Position 
        {
            get => transform.position;
            set => transform.position = value;
        }

        private FireballBigPres _presenter;

        private void Awake() {
            _presenter = new FireballBigPres(this);
        }
    }


    public class FireballBigPres : BaseEntity
    {

        public TypeId owner { get; set; }
        private IFireballView _view;
        private CharacterModel _character;

        public FireballBigPres(IFireballView view):base(ENTITY_TYPE.SPELL_FIREBALL_BIG)
        {
            _character = CharacterModel.Create(Type, Id);

            _view = view;
            _character.Position = view.Position;
            _character.Subscribe(OnCharacterChanged);
        }

        public override void OnUpdate(float dt)
        {

        }

        public override void OnDelete()
        {
            _character.UnSubscribes();
        }


        private void OnCharacterChanged(int change) 
        {
            switch ((CharacterModel.CHANGE)change)
            {
                case CharacterModel.CHANGE.POSITION:
                    _view.Position = _character.Position;
                    break;
            }
        }
    }
}