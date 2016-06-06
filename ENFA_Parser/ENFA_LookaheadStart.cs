using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_LookaheadStart: ENFA_GroupingStart
    {
        //A state can be a lookahead start
        public ENFA_LookaheadStart(StateType stateType) : base(stateType)
        { }
    }
}
