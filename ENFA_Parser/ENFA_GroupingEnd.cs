using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public abstract class ENFA_GroupingEnd:ENFA_Base
    {
        public ENFA_GroupingEnd():base(StateType.NotApplicable)
        { }
    }
}
