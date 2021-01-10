using System.Collections.Generic;
using System.Linq;

namespace Entities
{

    public abstract class FSM
    {

        private Dictionary<string,IFSMState> _states = new Dictionary<string, IFSMState>();
        private List<IFSMState> _sortStates = new List<IFSMState>();
        private IFSMState _curState;
        public AnimationManager Animation { get; }

        public FSM(AnimationManager animationManager)
        {
            Animation = animationManager;
        }

        protected void AddState(IFSMState state)
        {
            _states.Add(state.Name, state);
            _sortStates.Clear();
            _sortStates = (from s in _states orderby s.Value.Priority ascending select s.Value).ToList();
        }

        protected IFSMState GetState(string stateName)
        {
            if (_states.ContainsKey(stateName))
                return _states[stateName];
            
            return null;
        }

        public virtual void ChangeState(string stateName)
        {
            
            if (_curState != null && _curState.Name == stateName)
                return;
            
            _curState?.OnExit();
            _curState = GetState(stateName);
            _curState?.OnEnter();

            if (_curState == null)
                Logger.Error("FSM State: "+stateName+" not found");
        }


        public void Update(float dt)
        {
            CheckoutStates(dt);
            _curState?.OnUpdate(dt);
        }

        // public abstract void CheckoutStates(float dt);

        public void CheckoutStates(float dt)
        {
            foreach (var state in _sortStates)
            {
                if (state.CanEnter())
                {
                    ChangeState(state.Name);
                    break;
                }
            }
        }
    }
}