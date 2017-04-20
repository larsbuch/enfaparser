using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public abstract class ENFA_GroupingEnd:ENFA_Base
    {
        private ENFA_GroupingEnd _parent;

        public ENFA_GroupingEnd(ENFA_GroupingEnd parent) :base(StateType.NotApplicable)
        { }

        public ENFA_GroupingEnd Parent
        {
            get
            {
                return _parent;
            }
        }
    }
}
