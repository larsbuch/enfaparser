using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_ScopeEnd: ENFA_Base
    {
        //A state# can be a scope end (pops scope off scope stack)
        public ENFA_ScopeEnd(ENFA_Controller controller, StateType stateType) : base(controller, stateType)
        { }
    }
}
