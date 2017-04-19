using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public abstract class ENFA_GroupingStart:ENFA_Base
    {
        public ENFA_GroupingStart():base(StateType.NotApplicable)
        { }
    }
}
