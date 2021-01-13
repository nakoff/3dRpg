using Game;
using UnityEngine;
using Datas;

namespace Entities.PlayerState
{

    public class StateIdle:IFSMState
    {

        public string Name { get; }
        public int Priority { get; }

        private PlayerFSM _fsm;
        private Vector2 _rotation;
        private InputSettingsModel _inputSetting;
        private CharacterModel _character;

        public StateIdle(string name, PlayerFSM fsm, int priotity)
        {
            Name = name;
            _fsm = fsm;
            Priority = priotity;

            _character = Datas.CharacterModel.GetByParent(_fsm.player.Type, _fsm.player.Id);
            if (_character == null)
                Logger.Error("wrong model");
            
            _inputSetting = InputSettingsModel.Get();
            if (_inputSetting == null)
                Logger.Error("InputSettings is not exists");
        }

        public bool CanEnter()
        {
            return true;
        }

        public void OnUpdate(float dt)
        {
            Rot(dt);
        }

        public void OnEnter()
        {
            _fsm.ChangeAnimation(PlayerFSM.ANIMATION.IDLE);
            _rotation = _character.Rotation;
        }

        public void OnExit()
        {
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