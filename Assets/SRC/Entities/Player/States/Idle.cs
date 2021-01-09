

namespace Entities
{

    public class StateIdle:IFSMState
    {

        public string Name { get; }

        private PlayerFSM _fsm;

        public StateIdle(string name, PlayerFSM fsm)
        {
            Name = name;
            _fsm = fsm;
        }

        public void OnUpdate(float dt)
        {
        }

        public void OnEnter()
        {
            _fsm.ChangeAnimation(PlayerFSM.ANIMATION.IDLE);
        }

        public void OnExit()
        {
        }

    }
}