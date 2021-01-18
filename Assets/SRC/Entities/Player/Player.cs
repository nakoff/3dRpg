using System;
using Datas;
using Game;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerPres: BaseEntity
    {
        private IPlayerView _view;
        // private IAnimatedView _playerAnim;
        private CharacterModel _character;
        private InputSettingsModel _inputSetting;
        private AnimStateModel _animState;
        private AnimController _animController;
        private PlayerFSM _fsm;


        public PlayerPres(IPlayerView view):base(ENTITY_TYPE.PLAYER)
        {
            _character = CharacterModel.Create(Type, Id);
            _character.MoveSpeed = 100;
            _character.Subscribe(OnCharacterChanged);

            var collState = InteractStateModel.Create(Type, Id);
            var interactController = new InteractController(collState.obj);

            _view = view;
            _view.interactController = interactController;
            _character.Position = _view.Position;
            _view.Subscribe(OnViewChanged);

            _animState = AnimStateModel.Create(Type, Id);
            _animController = _view.CreateAnimController(_animState.obj);

            AddAnimation(Const.ANIMATION.IDLE, "Standing Idle");
            AddAnimation(Const.ANIMATION.WALK_FW, "Standing Walk Forward");
            AddAnimation(Const.ANIMATION.WALK_BW, "Standing Walk Back");
            AddAnimation(Const.ANIMATION.WALK_LEFT, "Standing Walk Left");
            AddAnimation(Const.ANIMATION.WALK_RIGHT, "Standing Walk Right");
            AddAnimation(Const.ANIMATION.RUN_FW, "Standing Run Forward");
            AddAnimation(Const.ANIMATION.RUN_BW, "Standing Run Back");
            AddAnimation(Const.ANIMATION.RUN_LEFT, "Standing Run Left");
            AddAnimation(Const.ANIMATION.RUN_RIGHT, "Standing Run Right");
            AddAnimation(Const.ANIMATION.ATTACK_FIREBALL_BIG, "Standing 1H Magic Attack 01");
            AddAnimation(Const.ANIMATION.ATTACK_FIREBALL_SMALL, "Standing 2H Magic Attack 01");
            AddAnimation(Const.ANIMATION.ATTACK_SPELL_GROUND, "Standing 2H Cast Spell 01");

            _fsm = new PlayerFSM(this, _animState.obj, _character.obj);
            
        }

        private void AddAnimation(Const.ANIMATION key, string animation)
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
            _fsm.OnDelete();
        }
    }
}