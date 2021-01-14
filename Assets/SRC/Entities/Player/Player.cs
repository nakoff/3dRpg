using System;
using Datas;
using Game;
using UnityEngine;

namespace Entities
{
    public class Player: BaseEntity
    {
        private IPlayerView _view;
        // private IAnimatedView _playerAnim;
        private CharacterModel _character;
        private InputSettingsModel _inputSetting;
        private AnimStateModel _animState;
        private AnimController _animController;
        private FSM _fsm;


        public Player(IPlayerView view):base(ENTITY_TYPE.PLAYER)
        {
            _character = CharacterModel.Create(Type, Id);
            _character.MoveSpeed = 100;
            _character.Subscribe(OnCharacterChanged);

            _view = view;
            _character.Position = _view.Position;
            _view.Subscribe(OnViewChanged);

            _animState = AnimStateModel.Create(Type, Id);
            _animController = _view.CreateAnimController(_animState.obj);

            AddAnimation(PlayerConst.ANIMATION.IDLE, "Standing Idle");
            AddAnimation(PlayerConst.ANIMATION.WALK_FW, "Standing Walk Forward");
            AddAnimation(PlayerConst.ANIMATION.WALK_BW, "Standing Walk Back");
            AddAnimation(PlayerConst.ANIMATION.WALK_LEFT, "Standing Walk Left");
            AddAnimation(PlayerConst.ANIMATION.WALK_RIGHT, "Standing Walk Right");
            AddAnimation(PlayerConst.ANIMATION.RUN_FW, "Standing Run Forward");
            AddAnimation(PlayerConst.ANIMATION.RUN_BW, "Standing Run Back");
            AddAnimation(PlayerConst.ANIMATION.RUN_LEFT, "Standing Run Left");
            AddAnimation(PlayerConst.ANIMATION.RUN_RIGHT, "Standing Run Right");
            AddAnimation(PlayerConst.ANIMATION.ATTACK_FIREBALL_BIG, "Standing 1H Magic Attack 01");
            AddAnimation(PlayerConst.ANIMATION.ATTACK_FIREBALL_SMALL, "Standing 2H Magic Attack 01");
            AddAnimation(PlayerConst.ANIMATION.ATTACK_SPELL_GROUND, "Standing 2H Cast Spell 01");

            _fsm = new PlayerFSM(this, _animState.obj);
            
        }

        private void AddAnimation(PlayerConst.ANIMATION key, string animation)
        {
            var success = _animController.AddAnimation((int)key, animation);
            if (!success)
                Logger.Error("Cant add animation, key:"+key.ToString());
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
                
            }
        }

        public override void OnUpdate(float dt)
        {
            _view.Movement(new Vector2(_character.Moving.x, _character.Moving.z), _character.MoveSpeed, dt);
            _view.Rotate(_character.Rotation, dt);

            _fsm.Update(dt);
            _animController.Update(dt);
        }


        public override void OnDelete()
        {
            _character.UnSubscribes();
        }
    }
}