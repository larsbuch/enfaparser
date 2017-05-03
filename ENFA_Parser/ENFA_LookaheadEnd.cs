using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_LookaheadEnd: ENFA_GroupingEnd
    {
        private ENFA_LookaheadStart _lookaheadStart;

        //A state can be a lookahead end
        public ENFA_LookaheadEnd(ENFA_Controller controller, ENFA_LookaheadStart lookaheadStart, ENFA_GroupingEnd parent) : base(controller, false, null, parent)
        {
            _lookaheadStart = lookaheadStart;
        }

        public ENFA_LookaheadStart LookaheadStart
        {
            get
            {
                return _lookaheadStart;
            }
        }

        internal override void RegisterGroupName(string groupName)
        {
            if (Parent != null)
            {
                Parent.RegisterGroupName(groupName);
            }
        }
    }
}
