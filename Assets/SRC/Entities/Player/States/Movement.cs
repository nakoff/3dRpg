
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
        private PlayerFSM.ANIMATION _curAnim;
        private Datas.CharacterModel _character;
        private Vector2 _rotation;
        private InputSettingsModel _inputSetting;

        public StateMovement(string name, PlayerFSM fsm, int priority)
        {
            Name = name;
            _fsm = fsm;
            Priority = priority;

            _character = Datas.CharacterModel.GetByParent(_fsm.player.Type, _fsm.player.Id);
            if (_character == null)
                Logger.Error("wrong model");
            
            _inputSetting = InputSettingsModel.Get();
            if (_inputSetting == null)
                Logger.Error("InputSettings is not exists");
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
            _rotation = _character.Rotation;
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
            Move(dt);
            Rot(dt);
        }

        public void OnExit()
        {
            _character.Moving = Vector3.zero;
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

        private void Move(float dt)
        {

            var moving = Vector3.zero;
            moving.x = InputManager.GetValue(InputManager.ACTIONS.MOVE_X);
            moving.z = InputManager.GetValue(InputManager.ACTIONS.MOVE_Z);

            _character.Moving = moving;
        }

        private void Rot(float dt)
        {
            _rotation.y += InputManager.GetValue(InputManager.ACTIONS.MOUSE_X) * _inputSetting.MouseSens * dt;
            _rotation.x -= InputManager.GetValue(InputManager.ACTIONS.MOUSE_Y) * _inputSetting.MouseSens * dt;
            _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);
            _character.Rotation = _rotation;
        }

    }
}