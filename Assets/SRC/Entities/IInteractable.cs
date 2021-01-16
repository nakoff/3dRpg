

namespace Entities
{

    public interface IInteractable
    {
        ENTITY_TYPE EntityType { get;}
        uint EntityId { get; }

        void Init(ENTITY_TYPE type, uint id, InteractController interactController);
    }
}