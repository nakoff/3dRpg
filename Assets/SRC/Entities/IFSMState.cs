
namespace Entities
{

    public interface IFSMState
    {
        string Name { get; }
        void OnEnter();
        void OnExit();
        void OnUpdate(float dt);
    }
}