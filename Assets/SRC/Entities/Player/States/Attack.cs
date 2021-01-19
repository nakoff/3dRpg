

namespace Entities.Player
{

    public class StateAttack:IFSMState
    {

        public string Name { get; }
        public int Priority { get; }

        private PlayerFSM _fsm;
        private Models.CharacterModel _character;
        private Const.ANIMATION _curAnim;
        private bool _isAnimationPlaying;

        public StateAttack(string name, PlayerFSM fsm, int priotity)
        {
            Name = name;
            _fsm = fsm;
            Priority = priotity;

            _character = new Models.CharacterModel(_fsm.charObj);
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
        }


        public void OnExit()
        {
            _fsm.animationFinished -= OnAnimationFinished;
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