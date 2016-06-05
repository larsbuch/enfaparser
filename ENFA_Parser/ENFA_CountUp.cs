using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_CountUp: ENFA_Base
    {
        //A state can be a countup state (counter has unique id)
        public ENFA_CountUp(StateType stateType) : base(stateType)
        { }
    }
}
