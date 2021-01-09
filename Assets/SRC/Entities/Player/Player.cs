using System;
using Datas;
using Game;
using UnityEngine;

namespace Entities
{
    public class Player: BaseEntity
    {
        private IPlayerView _view;
        private IAnimatedView _playerAnim;
        private CharacterModel _character;
        private InputSettingsModel _inputSetting;
        private FSM _fsm;

        private Vector2 _rotation;

        public Player(IPlayerView view, IAnimatedView playerAnim):base(ENTITY_TYPE.PLAYER)
        {
            _character = CharacterModel.Create(Type, Id);
            _character.MoveSpeed = 100;
            _character.Subscribe(OnCharacterChanged);

            _view = view;
            _playerAnim = playerAnim;
            _character.Position = _view.Position;
            _view.Subscribe(OnViewChanged);

            _inputSetting = InputSettingsModel.Get();
            if (_inputSetting == null)
                Logger.Error("InputSettings is not exists");

            var animationSystem = new AnimationManager(newAnim => _character.CurAnimation = newAnim);
            _fsm = new PlayerFSM(animationSystem);
            
        }


        private void OnViewChanged(IPlayerView.CHANGED change)
        {
            switch (change)
            {
                case IPlayerView.CHANGED.POSITION:
                    _character.Position = _view.Position;
                    break;
            }
        }

        private void OnCharacterChanged(int change)
        {
            switch ((CharacterModel.CHANGE)change)
            {
                case CharacterModel.CHANGE.POSITION:
                    break;

                case CharacterModel.CHANGE.HEALTH:
                    break;
                
                case CharacterModel.CHANGE.MOVING:
                    break;
                
                case CharacterModel.CHANGE.ANIMATION:
                    _playerAnim.ChangeAnimation(_character.CurAnimation);
                    break;
            }
        }

        public override void OnUpdate(float dt)
        {
            _rotation.y += InputManager.GetValue(InputManager.ACTIONS.MOUSE_X) * _inputSetting.MouseSens * dt;
            _rotation.x -= InputManager.GetValue(InputManager.ACTIONS.MOUSE_Y) * _inputSetting.MouseSens * dt;
            _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);

            var moveX = InputManager.GetValue(InputManager.ACTIONS.MOVE_X);
            var moveZ = InputManager.GetValue(InputManager.ACTIONS.MOVE_Z);

            _view.Movement(new Vector2(moveX, moveZ), _character.MoveSpeed, dt);
            _view.Rotate(_rotation, dt);

            _fsm.Update(dt);
        }


        public override void OnDelete()
        {
            _character.UnSubscribes();
        }
    }
}