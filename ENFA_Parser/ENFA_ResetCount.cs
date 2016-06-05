using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_ResetCount: ENFA_Base
    {
        //A state can be a reset count state (for unique id)
        public ENFA_ResetCount(StateType stateType) : base(stateType)
        { }
    }
}
