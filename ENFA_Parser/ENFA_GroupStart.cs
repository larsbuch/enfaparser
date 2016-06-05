using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_GroupStart: ENFA_Base
    {
        //A state can be a group start (recording* or non recording)
        public ENFA_GroupStart(StateType stateType) : base(stateType)
        { }
    }
}
