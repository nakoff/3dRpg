

namespace Entities
{

    public class StateWalk:IFSMState
    {

        public string Name { get; }

        private PlayerFSM _fsm;

        public StateWalk(string name, PlayerFSM fsm)
        {
            Name = name;
            _fsm = fsm;
        }

        public void OnUpdate(float dt)
        {
        }

        public void OnEnter()
        {
            _fsm.ChangeAnimation(PlayerFSM.ANIMATION.WALK_FW);
        }

        public void OnExit()
        {
        }

    }
}