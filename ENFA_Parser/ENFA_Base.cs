using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public abstract class ENFA_Base
    {
        //A state can be a negating state which terminates the state-visitor path
        //A state can be an accepting state which is a match
        //A state can be a transition state(not negating or accepting)

        private StateType _stateType;
        private List<ENFA_Transition> _nextTransitions;

        public ENFA_Base(StateType stateType)
        {
            _stateType = stateType;
            _nextTransitions = new List<ENFA_Transition>();
        }

        public StateType StateType
        {
            get
            {
                return _stateType;
            }
        }

        public virtual string StateName
        {
            get
            {
                return GetType().Name;
            }
        }

        public void AddTransition(ENFA_Transition nextTransition)
        {
            _nextTransitions.Add(nextTransition);
        }

        public List<ENFA_Transition> GetTransitions
        {
            get
            {
                return _nextTransitions;
            }
        }
    }
}
