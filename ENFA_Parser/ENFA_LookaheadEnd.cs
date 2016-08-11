using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_LookaheadEnd: ENFA_GroupingEnd
    {
        //A state can be a lookahead end
        public ENFA_LookaheadEnd(StateType stateType) : base(stateType)
        { }
    }
}
