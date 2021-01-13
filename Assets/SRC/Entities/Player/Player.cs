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
        private AnimationController _animController;
        private FSM _fsm;


        public Player(IPlayerView view, IAnimatedView playerAnim):base(ENTITY_TYPE.PLAYER)
        {
            _character = CharacterModel.Create(Type, Id);
            _character.MoveSpeed = 100;
            _character.Subscribe(OnCharacterChanged);

            _view = view;
            _playerAnim = playerAnim;
            _playerAnim.Subscribe(change =>
            {
                if (change == IAnimatedView.CHANGED.ANIMATION_FINISHED)
                    _animController?.OnAnimationFinished();
            });

            _character.Position = _view.Position;
            _view.Subscribe(OnViewChanged);

            

            _animController = new AnimationController();
            _animController.changed += OnAnimationConttollerChanged;

            _fsm = new PlayerFSM(_animController, this);
            
        }


        private void OnAnimationConttollerChanged() => _character.CurAnimation = _animController.GetAnimation(_animController.CurAnimationKey); 

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
                
                case CharacterModel.CHANGE.ANIMATION:
                    _playerAnim.ChangeAnimation(_character.CurAnimation);
                    break;
            }
        }

        public override void OnUpdate(float dt)
        {
            _view.Movement(new Vector2(_character.Moving.x, _character.Moving.z), _character.MoveSpeed, dt);
            _view.Rotate(_character.Rotation, dt);

            _fsm.Update(dt);
        }


        public override void OnDelete()
        {
            _character.UnSubscribes();
            _animController.changed -= OnAnimationConttollerChanged;
        }
    }
}