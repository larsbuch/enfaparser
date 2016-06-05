using System;
using System.Collections.Generic;
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
        public ENFA_Base(StateType stateType)
        {
            _stateType = stateType;
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
    }
}
