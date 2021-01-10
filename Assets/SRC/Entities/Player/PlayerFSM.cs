using System.Collections.Generic;
using Entities.PlayerState;

namespace Entities
{

    public class PlayerFSM:FSM
    {

        public Player player { get; }
        public enum STATE { IDLE, WALK, }
        public enum ANIMATION { 
            IDLE, WALK_FW, WALK_BW, WALK_LEFT, WALK_RIGHT, 
            RUN_FW, RUN_BW, RUN_LEFT, RUN_RIGHT,
        }
        
        
        public PlayerFSM(AnimationManager animationManager, Player player):base(animationManager)
        {
            this.player = player;

            AddState( new StateIdle(STATE.IDLE.ToString(), this, 1000) );
            AddState( new StateMovement(STATE.WALK.ToString(), this, 50) );

            AddAnimation(ANIMATION.IDLE, "Standing Idle");
            AddAnimation(ANIMATION.WALK_FW, "Standing Walk Forward");
            AddAnimation(ANIMATION.WALK_BW, "Standing Walk Back");
            AddAnimation(ANIMATION.WALK_LEFT, "Standing Walk Left");
            AddAnimation(ANIMATION.WALK_RIGHT, "Standing Walk Right");
            AddAnimation(ANIMATION.RUN_FW, "Standing Run Forward");
            AddAnimation(ANIMATION.RUN_BW, "Standing Run Back");
            AddAnimation(ANIMATION.RUN_LEFT, "Standing Run Left");
            AddAnimation(ANIMATION.RUN_RIGHT, "Standing Run Right");

            ChangeState(STATE.IDLE);
        }

        public void ChangeState(STATE state) => ChangeState(state.ToString());
        private void AddAnimation(ANIMATION animName, string animation) => Animation.AddAnimation(animName.ToString(), animation);
        public void ChangeAnimation(ANIMATION animName) => Animation.ChangeAnimation(animName.ToString());

    }

}