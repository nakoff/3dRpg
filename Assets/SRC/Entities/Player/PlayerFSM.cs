using System;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;
using Game;
using Models;

namespace Entities.Player
{

    public class PlayerFSM:FSM
    {

        public PlayerPres player { get; }
        public CharacterObject charObj => _character.obj;
        public AnimStateObject animStateObj => _animState.obj;
        public enum STATE { IDLE, WALK, ATTACK, }

        public System.Action animationFinished = delegate{};

        private AnimStateModel _animState; 
        private CharacterModel _character;
        private InputSettingsModel _inputSetting;
        private Vector2 _playerRotation;
        
        public PlayerFSM(PlayerPres player, AnimStateObject animObj, CharacterObject charObj):base()
        {
            this.player = player;
            _character = new CharacterModel(charObj);
            _playerRotation = _character.Rotation;

            _animState = new AnimStateModel(animObj);
            _animState.Subscribe(change =>
            {
                switch ((AnimStateModel.CHANGE)change)
                {
                    case AnimStateModel.CHANGE.IS_PLAYING:
                        if (!_animState.IsPlaying)
                            OnAnimationFinished();
                        break;
                }
            });

            _inputSetting = InputSettingsModel.Get();
            if (_inputSetting == null)
                Logger.Error("obj not found");

            AddState( new StateAttack(STATE.ATTACK.ToString(), this, 40) );
            AddState( new StateMovement(STATE.WALK.ToString(), this, 50) );
            AddState( new StateIdle(STATE.IDLE.ToString(), this, 1000) );

            ChangeState(STATE.IDLE);
        }

        public Const.ANIMATION CurAnimation => (Const.ANIMATION)_animState.CurAnimation;
        public void ChangeState(STATE state) => ChangeState(state.ToString());
        public void ChangeAnimation(Const.ANIMATION key) => _animState.CurAnimation = (int)key;

        public void PlayerMove(float dt)
        {
            var moving = Vector3.zero;
            moving.x = InputManager.GetValue(InputManager.ACTIONS.MOVE_X);
            moving.z = InputManager.GetValue(InputManager.ACTIONS.MOVE_Z);

            _character.Moving = moving;
        }

        public void PlayerRot(float dt)
        {
            _playerRotation.y += InputManager.GetValue(InputManager.ACTIONS.MOUSE_X) * _inputSetting.MouseSens * dt;
            _playerRotation.x -= InputManager.GetValue(InputManager.ACTIONS.MOUSE_Y) * _inputSetting.MouseSens * dt;
            _playerRotation.x = Mathf.Clamp(_playerRotation.x, -90, 90);
            _character.Rotation = _playerRotation;
        }

        private void OnAnimationFinished() => animationFinished(); 


        public void OnDelete()
        {
            _animState.UnSubscribes();
        }

    }

}