
using Game;
using Datas;
using UnityEngine;

namespace Entities.PlayerState
{

    public class StateMovement:IFSMState
    {

        public string Name { get; }
        public int Priority { get; }

        private PlayerFSM _fsm;
        private PlayerConst.ANIMATION _curAnim;
        private Datas.CharacterModel _character;

        public StateMovement(string name, PlayerFSM fsm, int priority)
        {
            Name = name;
            _fsm = fsm;
            Priority = priority;

            _character = new CharacterModel(_fsm.charObj);
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
            _fsm.PlayerMove(dt);
            _fsm.PlayerRot(dt);
        }

        public void OnExit()
        {
            _character.Moving = Vector3.zero;
        }


        private void Walk(float moveX, float moveZ)
        {
            if (moveZ != 0)
                _curAnim = moveZ >0 ? PlayerConst.ANIMATION.WALK_FW : PlayerConst.ANIMATION.WALK_BW;
            else if (moveX != 0)
                _curAnim = moveX >0 ? PlayerConst.ANIMATION.WALK_RIGHT: PlayerConst.ANIMATION.WALK_LEFT;

            _character.MoveSpeed = PlayerConst.WalkSpeed;
        }

        private void Run(float moveX, float moveZ)
        {
            if (moveZ != 0)
                _curAnim = moveZ >0 ? PlayerConst.ANIMATION.RUN_FW : PlayerConst.ANIMATION.RUN_BW;
            else if (moveX != 0)
                _curAnim = moveX >0 ? PlayerConst.ANIMATION.RUN_RIGHT: PlayerConst.ANIMATION.RUN_LEFT;

            _character.MoveSpeed = PlayerConst.RunSpeed;
        }



    }
}