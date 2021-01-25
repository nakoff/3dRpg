using Models;

namespace Entities.Player
{

    public class StateAttack:IFSMState
    {

        public string Name { get; }
        public int Priority { get; }

        private PlayerFSM _fsm;
        private CharacterModel _character;
        private Const.ANIMATION _curAnim;
        private AnimStateModel _animState;
        private bool _isAnimationPlaying;
        private CharacterModel _spellCharacter;

        public StateAttack(string name, PlayerFSM fsm, int priotity)
        {
            Name = name;
            _fsm = fsm;
            Priority = priotity;

            _character = new CharacterModel(_fsm.charObj);
        }

        public bool CanEnter()
        {
            var attack1 = Game.InputManager.GetValue(Game.InputManager.ACTIONS.ATTACK_LEFT);
            var attack2 = Game.InputManager.GetValue(Game.InputManager.ACTIONS.ATTACK_RIGHT);
            
            if (attack1 > 0 || attack2 > 0 || _isAnimationPlaying)
                return true;
            
            return false;
        }


        public void OnEnter()
        {
            _curAnim = Const.ANIMATION.IDLE;
            _fsm.animationFinished += OnAnimationFinished;
            _animState = new AnimStateModel(_fsm.animStateObj);
            _animState.Subscribe(OnAnimStateChanged);
        }


        public void OnUpdate(float dt)
        {
            var attack1 = Game.InputManager.GetValue(Game.InputManager.ACTIONS.ATTACK_LEFT);
            var attack2 = Game.InputManager.GetValue(Game.InputManager.ACTIONS.ATTACK_RIGHT);
            var spetial = Game.InputManager.GetValue(Game.InputManager.ACTIONS.SPECIAL);
            var characterState = (PlayerFSM.STATE)_character.CurState;

            
            if (attack1 > 0 && !_isAnimationPlaying)
            {
                FireballBig();
            } 
            else if (attack2 > 0 && !_isAnimationPlaying)
            {
                FireballSmall();
            }

            _fsm.ChangeAnimation(_curAnim);
            _fsm.PlayerRot(dt);

            if (_spellCharacter != null)
                _spellCharacter.Position = _fsm.player.FistPosition;
        }


        public void OnExit()
        {
            _fsm.animationFinished -= OnAnimationFinished;
            _animState.UnSubscribes();
        }


        private void FireballBig()
        {
            _isAnimationPlaying = true;
            _curAnim = Const.ANIMATION.ATTACK_FIREBALL_BIG;

        }
        
        private void FireballSmall()
        {
            _isAnimationPlaying = true;
            _curAnim = Const.ANIMATION.ATTACK_FIREBALL_SMALL;

        }

        private void OnAnimStateChanged(int change) 
        {
            switch ((AnimStateModel.CHANGE)change)
            {
                case AnimStateModel.CHANGE.ACTION:
                    CheckoutAnimAction(_animState.Action);
                    break;
                
                case AnimStateModel.CHANGE.CUR_ANIMATION:
                    if (_animState.CurAnimation == (int)Const.ANIMATION.ATTACK_FIREBALL_BIG)
                    {
                        var typeId = EntityFactory.CreateFireball();
                        _spellCharacter = CharacterModel.GetByParent(typeId.Type, typeId.Id);
                        _spellCharacter.Position = _fsm.player.FistPosition;
                    }
                    break;
            }
        }

        private void CheckoutAnimAction(int action) 
        {
            var a = (ANIMATION_EVENT)action;
            if (a == ANIMATION_EVENT.PLAYER_FIREBALL_BIG)
            {
                _spellCharacter = null;
            }
        }

        private void OnAnimationFinished()
        {
            if (_fsm.CurAnimation == _curAnim)
            {
                _isAnimationPlaying = false;
                _fsm.ChangeAnimation(Const.ANIMATION.IDLE);
                Logger.Print("Attack Finished");
            }
        }

    }
}