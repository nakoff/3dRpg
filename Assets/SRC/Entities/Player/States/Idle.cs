using Game;
using UnityEngine;
using Datas;

namespace Entities.Player
{

    public class StateIdle:IFSMState
    {

        public string Name { get; }
        public int Priority { get; }

        private PlayerFSM _fsm;

        public StateIdle(string name, PlayerFSM fsm, int priotity)
        {
            Name = name;
            _fsm = fsm;
            Priority = priotity;
            
        }

        public bool CanEnter()
        {
            return true;
        }

        public void OnUpdate(float dt)
        {
            _fsm.PlayerRot(dt);
        }

        public void OnEnter()
        {
            _fsm.ChangeAnimation(Const.ANIMATION.IDLE);
        }

        public void OnExit()
        {
        }

    }
}