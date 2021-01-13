using System.Collections.Generic;
using Entities.PlayerState;

namespace Entities
{

    public class PlayerFSM:FSM
    {

        public Player player { get; }
        public enum STATE { IDLE, WALK, ATTACK, }

        public AnimationController Animation { get; }
        public enum ANIMATION { 
            IDLE, WALK_FW, WALK_BW, WALK_LEFT, WALK_RIGHT, 
            RUN_FW, RUN_BW, RUN_LEFT, RUN_RIGHT,
            ATTACK_FIREBALL_BIG, ATTACK_FIREBALL_SMALL, ATTACK_SPELL_GROUND,
        }
        
        
        public PlayerFSM(AnimationController animationController, Player player):base()
        {
            Animation = animationController;
            this.player = player;

            AddState( new StateAttack(STATE.ATTACK.ToString(), this, 40) );
            AddState( new StateMovement(STATE.WALK.ToString(), this, 50) );
            AddState( new StateIdle(STATE.IDLE.ToString(), this, 1000) );

            AddAnimation(ANIMATION.IDLE, "Standing Idle");
            AddAnimation(ANIMATION.WALK_FW, "Standing Walk Forward");
            AddAnimation(ANIMATION.WALK_BW, "Standing Walk Back");
            AddAnimation(ANIMATION.WALK_LEFT, "Standing Walk Left");
            AddAnimation(ANIMATION.WALK_RIGHT, "Standing Walk Right");
            AddAnimation(ANIMATION.RUN_FW, "Standing Run Forward");
            AddAnimation(ANIMATION.RUN_BW, "Standing Run Back");
            AddAnimation(ANIMATION.RUN_LEFT, "Standing Run Left");
            AddAnimation(ANIMATION.RUN_RIGHT, "Standing Run Right");
            AddAnimation(ANIMATION.ATTACK_FIREBALL_BIG, "Standing 1H Magic Attack 01");
            AddAnimation(ANIMATION.ATTACK_FIREBALL_SMALL, "Standing 2H Magic Attack 01");
            AddAnimation(ANIMATION.ATTACK_SPELL_GROUND, "Standing 2H Cast Spell 01");

            ChangeState(STATE.IDLE);
        }

        public void ChangeState(STATE state) => ChangeState(state.ToString());
        private void AddAnimation(ANIMATION key, string animation) => Animation.AddAnimation(key.ToString(), animation);
        public string GetAnimation(ANIMATION key) => Animation.GetAnimation(key.ToString());
        public void ChangeAnimation(ANIMATION key) => Animation.ChangeAnimation(key.ToString());

    }

}