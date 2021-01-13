

namespace Entities.PlayerState
{

    public class StateAttack:IFSMState
    {

        public string Name { get; }
        public int Priority { get; }

        private PlayerFSM _fsm;
        private Datas.CharacterModel _character;
        private PlayerFSM.ANIMATION _curAnim;
        private bool _isAnimationPlaying;

        public StateAttack(string name, PlayerFSM fsm, int priotity)
        {
            Name = name;
            _fsm = fsm;
            Priority = priotity;

            _character = Datas.CharacterModel.GetByParent(_fsm.player.Type, _fsm.player.Id);
            if (_character == null)
                Logger.Error("wrong model");
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
            _curAnim = PlayerFSM.ANIMATION.IDLE;
            _fsm.Animation.finished += OnAnimationFinished;
        }


        public void OnUpdate(float dt)
        {
            var attack1 = Game.InputManager.GetValue(Game.InputManager.ACTIONS.ATTACK_LEFT);
            var attack2 = Game.InputManager.GetValue(Game.InputManager.ACTIONS.ATTACK_RIGHT);
            var spetial = Game.InputManager.GetValue(Game.InputManager.ACTIONS.SPECIAL);
            var characterAnim = _character.CurAnimation;
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
        }


        public void OnExit()
        {
            _fsm.Animation.finished -= OnAnimationFinished;
        }


        private void FireballBig()
        {
            _isAnimationPlaying = true;
            _curAnim = PlayerFSM.ANIMATION.ATTACK_FIREBALL_BIG;

        }
        
        private void FireballSmall()
        {
            _isAnimationPlaying = true;
            _curAnim = PlayerFSM.ANIMATION.ATTACK_FIREBALL_SMALL;

        }

        private void OnAnimationFinished()
        {
            if (_fsm.Animation.CurAnimationKey == _curAnim.ToString())
            {
                _isAnimationPlaying = false;
                Logger.Print("Attack Finished");
            }
        }

    }
}