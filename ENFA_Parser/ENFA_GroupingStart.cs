using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public abstract class ENFA_GroupingStart:ENFA_Base
    {
        private ENFA_GroupingStart _parent;

        public ENFA_GroupingStart(ENFA_GroupingStart parent) :base(StateType.NotApplicable)
        {
            _parent = parent;
        }

        public ENFA_GroupingStart Parent
        {
            get
            {
                return _parent;
            }
        }
    }
}
