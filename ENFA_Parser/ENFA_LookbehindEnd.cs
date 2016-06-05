using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_LookbehindEnd: ENFA_Base
    {
        //A state can be a lookbehind end
        public ENFA_LookbehindEnd(StateType stateType) : base(stateType)
        { }
    }
}
