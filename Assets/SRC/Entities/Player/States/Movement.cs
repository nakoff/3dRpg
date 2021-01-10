
namespace Entities.PlayerState
{

    public class StateMovement:IFSMState
    {

        public string Name { get; }
        public int Priority { get; }

        private PlayerFSM _fsm;
        private PlayerFSM.ANIMATION _curAnim;
        private Datas.CharacterModel _character;

        public StateMovement(string name, PlayerFSM fsm, int priority)
        {
            Name = name;
            _fsm = fsm;
            Priority = priority;

            _character = Datas.CharacterModel.GetByParent(_fsm.player.Type, _fsm.player.Id);
            if (_character == null)
                Logger.Error("wrong model");
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
        public void OnEnter()
        {
        }

        public void OnUpdate(float dt)
        {
            var moveX = Game.InputManager.GetValue(Game.InputManager.ACTIONS.MOVE_X);
            var moveZ = Game.InputManager.GetValue(Game.InputManager.ACTIONS.MOVE_Z);
            var sprint = Game.InputManager.GetValue(Game.InputManager.ACTIONS.SPECIAL);

            if (sprint != 0)
                Run(moveX, moveZ);
            else
                Walk(moveX, moveZ);

            
            _fsm.ChangeAnimation(_curAnim);
        }

        public void OnExit()
        {
        }


        private void Walk(float moveX, float moveZ)
        {
            if (moveZ != 0)
                _curAnim = moveZ >0 ? PlayerFSM.ANIMATION.WALK_FW : PlayerFSM.ANIMATION.WALK_BW;
            else if (moveX != 0)
                _curAnim = moveX >0 ? PlayerFSM.ANIMATION.WALK_RIGHT: PlayerFSM.ANIMATION.WALK_LEFT;

            _character.MoveSpeed = PlayerConst.WalkSpeed;
        }

        private void Run(float moveX, float moveZ)
        {
            if (moveZ != 0)
                _curAnim = moveZ >0 ? PlayerFSM.ANIMATION.RUN_FW : PlayerFSM.ANIMATION.RUN_BW;
            else if (moveX != 0)
                _curAnim = moveX >0 ? PlayerFSM.ANIMATION.RUN_RIGHT: PlayerFSM.ANIMATION.RUN_LEFT;

            _character.MoveSpeed = PlayerConst.RunSpeed;
        }

    }
}