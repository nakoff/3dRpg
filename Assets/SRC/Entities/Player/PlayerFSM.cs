using System;
using System.Collections.Generic;
using Entities.PlayerState;

namespace Entities
{

    public class PlayerFSM:FSM
    {

        public Player player { get; }
        public enum STATE { IDLE, WALK, ATTACK, }

        public System.Action animationFinished = delegate{};

        private Datas.AnimStateModel _animState; 
        
        public PlayerFSM(Player player, Datas.AnimStateObject obj):base()
        {
            this.player = player;
            _animState = new Datas.AnimStateModel(obj);
            _animState.Subscribe(change =>
            {
                switch ((Datas.AnimStateModel.CHANGE)change)
                {
                    case Datas.AnimStateModel.CHANGE.IS_PLAYING:
                        if (!_animState.IsPlaying)
                            OnAnimationFinished();
                        break;
                }
            });

            AddState( new StateAttack(STATE.ATTACK.ToString(), this, 40) );
            AddState( new StateMovement(STATE.WALK.ToString(), this, 50) );
            AddState( new StateIdle(STATE.IDLE.ToString(), this, 1000) );

            ChangeState(STATE.IDLE);
        }

        public PlayerConst.ANIMATION CurAnimation => (PlayerConst.ANIMATION)_animState.CurAnimation;
        public void ChangeState(STATE state) => ChangeState(state.ToString());
        public void ChangeAnimation(PlayerConst.ANIMATION key) => _animState.CurAnimation = (int)key;

        private void OnAnimationFinished() => animationFinished(); 

    }

}