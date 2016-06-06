using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_LookbehindStart: ENFA_GroupingStart
    {
        //A state can be a lookbehind start
        public ENFA_LookbehindStart(StateType stateType) : base(stateType)
        { }
    }
}
