using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_ContextSwitch: ENFA_Base
    {
        //A state# can be a context switch to another grammar
        public ENFA_ContextSwitch(ENFA_Controller controller, StateType stateType) : base(controller, stateType)
        { }
    }
}
