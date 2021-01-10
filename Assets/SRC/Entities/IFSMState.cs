
namespace Entities
{

    public interface IFSMState
    {
        string Name { get; }
        int Priority { get; }
        bool CanEnter();
        void OnEnter();
        void OnExit();
        void OnUpdate(float dt);
    }
}