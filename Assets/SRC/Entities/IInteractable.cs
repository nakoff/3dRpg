

namespace Entities
{

    public interface IInteractable
    {
        ENTITY_TYPE EntityType { get;}
        uint EntityId { get; }
        InteractController interactController { get; set; }

    }
}