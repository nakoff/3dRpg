

namespace Entities.PlayerState
{

    public class StateMovement:IFSMState
    {

        public string Name { get; }
        public int Priority { get; }

        private PlayerFSM _fsm;

        public StateMovement(string name, PlayerFSM fsm, int priority)
        {
            Name = name;
            _fsm = fsm;
            Priority = priority;
        }

        public bool CanEnter()
        {
            if (Game.InputManager.GetValue(Game.InputManager.ACTIONS.MOVE_X) != 0
            || Game.InputManager.GetValue(Game.InputManager.ACTIONS.MOVE_Z) != 0)
            {
                return true;
            }

            return false;
        }

        public void OnUpdate(float dt)
        {
            var moveX = Game.InputManager.GetValue(Game.InputManager.ACTIONS.MOVE_X);
            var moveZ = Game.InputManager.GetValue(Game.InputManager.ACTIONS.MOVE_Z);
            var anim = PlayerFSM.ANIMATION.IDLE;

            if (moveZ != 0)
                anim = moveZ >0 ? PlayerFSM.ANIMATION.WALK_FW : PlayerFSM.ANIMATION.WALK_BW;
            else if (moveX != 0)
                anim = moveX >0 ? PlayerFSM.ANIMATION.WALK_RIGHT: PlayerFSM.ANIMATION.WALK_LEFT;
            
            _fsm.ChangeAnimation(anim);
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

    }
}