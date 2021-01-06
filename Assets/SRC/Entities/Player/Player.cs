using System;
using Datas;
using Game;
using UnityEngine;

namespace Entities
{
    public class Player: BaseEntity
    {
        private IPlayerView _view;
        private CharacterModel _character;

        public Player(IPlayerView view):base(ENTITY_TYPE.PLAYER)
        {
            Game.InputManager.changed += OnInputChanged;

            _character = CharacterModel.Create(Type, Id);
            _character.Subscribe(OnCharacterChanged);

            _view = view;
            _character.Position = _view.Position;
            _view.Subscribe(OnViewChanged);
            
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
            }
        }

        private void OnInputChanged(InputManager.ACTIONS key, int value)
        {
            switch (key)
            {
            }
        }

        public override void OnUpdate(float dt)
        {
            var rotX = InputManager.GetValue(InputManager.ACTIONS.MOUSE_X);
            var rotY = InputManager.GetValue(InputManager.ACTIONS.MOUSE_Y);
            var moveX = InputManager.GetValue(InputManager.ACTIONS.MOVE_X);
            var moveZ = InputManager.GetValue(InputManager.ACTIONS.MOVE_Z);

            _view.Movement(new Vector2(moveX, moveZ), new Vector2(rotX, rotY), dt);
        }


        public override void OnDelete()
        {
            _character.UnSubscribes();
            InputManager.changed -= OnInputChanged;
        }
    }
}