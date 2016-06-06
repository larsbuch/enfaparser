using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_Step
    {
        private ENFA_Regex_Transition _transition;
        private ENFA_Base _state;

        public ENFA_Step(ENFA_Regex_Transition transition, ENFA_Base state)
        {
            _transition = transition;
            _state = state;
        }

        public ENFA_Regex_Transition Transition
        {
            get
            {
                return _transition;
            }
        }

        public ENFA_Base State
        {
            get
            {
                return _state;
            }
        }
    }
}
