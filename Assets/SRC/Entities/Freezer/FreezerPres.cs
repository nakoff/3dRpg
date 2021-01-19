using Models;

namespace Entities.Freezer
{

    public class FreezerPres:BaseEntity 
    {

        private CharacterModel _character;
        private IFreezerView _view;
        private InteractSystem _interactSystem;

        public FreezerPres(IFreezerView view):base(ENTITY_TYPE.PLAYER)
        {
            _character = CharacterModel.Create(Type, Id);
            _character.Position = view.Position;

            var interactModel = InteractStateModel.Create(Type, Id);
            var interactController = new InteractController(interactModel.obj);

            _interactSystem = new InteractSystem(this, interactModel.obj);

            _view = view;
            _view.interactController = interactController;
        }

        public override void OnUpdate(float dt)
        {
            _interactSystem.OnUpdate(dt);
        }

        public override void OnDelete()
        {
            _character.UnSubscribes();
            _interactSystem.OnDelete();
        }
    }
}