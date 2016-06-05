using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_ScopeStart: ENFA_Base
    {
        //A state# can be a scope start (pushed scope on scope stack)
        public ENFA_ScopeStart(StateType stateType) : base(stateType)
        { }
    }
}
