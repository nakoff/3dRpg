using System.Collections.Generic;

namespace Entities
{

    public class PlayerFSM:FSM
    {

        public enum STATE { IDLE, WALK, }
        public enum ANIMATION { IDLE, WALK_FW, }
        
        
        public PlayerFSM(AnimationManager animationSystem):base(animationSystem)
        {
            AddState( new StateIdle(STATE.IDLE.ToString(), this) );
            AddState( new StateWalk(STATE.WALK.ToString(), this) );

            AddAnimation(ANIMATION.IDLE, "Standing Idle");
            AddAnimation(ANIMATION.WALK_FW, "Standing Run Forward");

            ChangeState(STATE.IDLE);
        }


        public override void CheckoutStates(float dt)
        {
            if (CheckWalk()){
                ChangeState(STATE.WALK);
                return;
            }

            ChangeState(STATE.IDLE);
        }

        public void ChangeState(STATE state) => ChangeState(state.ToString());
        private void AddAnimation(ANIMATION animName, string animation) => Animation.AddAnimation(animName.ToString(), animation);
        public void ChangeAnimation(ANIMATION animName) => Animation.ChangeAnimation(animName.ToString());

        private bool CheckWalk()
        {
            if (Game.InputManager.GetValue(Game.InputManager.ACTIONS.MOVE_X) != 0
            || Game.InputManager.GetValue(Game.InputManager.ACTIONS.MOVE_Z) != 0)
            {
                return true;
            }

            return false;
        }

    }

}