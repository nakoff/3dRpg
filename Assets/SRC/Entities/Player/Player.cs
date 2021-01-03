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
                    _view.Moving = _character.Moving;
                    break;
            }
        }

        private void OnInputChanged(InputManager.ACTIONS key, int value)
        {
            switch (key)
            {
                case InputManager.ACTIONS.MOVE_X:
                    _view.Move = new Vector2(value, _view.Move.y);
                    break;

                case InputManager.ACTIONS.MOVE_Z:
                    _view.Move = new Vector2(_view.Move.x, value);
                    break;

            }
        }

        public override void OnUpdate(float dt)
        {
            var x = InputManager.GetValue(InputManager.ACTIONS.MOUSE_X);
            var y = InputManager.GetValue(InputManager.ACTIONS.MOUSE_Y);
            _view.Rotation = new Vector2(x, y);
        }


        public override void OnDelete()
        {
            _character.UnSubscribes();
            InputManager.changed -= OnInputChanged;
        }
    }
}