using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_GroupEnd: ENFA_Base
    {
        public ENFA_GroupEnd(StateType stateType) :base(stateType)
        { }

        //A state can be a group end
    }
}
