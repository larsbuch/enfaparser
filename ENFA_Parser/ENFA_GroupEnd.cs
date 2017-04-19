using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_GroupEnd: ENFA_GroupingEnd
    {
        private ENFA_GroupStart _groupStart;
        private ENFA_GroupingEnd _parent;

        public ENFA_GroupEnd(ENFA_GroupStart groupStart, ENFA_GroupingEnd parent) :base()
        {
            _groupStart = groupStart;
            _parent = parent;
        }

        public ENFA_GroupStart GroupStart
        {
            get
            {
                return _groupStart;
            }
        }

        public ENFA_GroupingEnd Parent
        {
            get
            {
                return _parent;
            }
        }

        //A state can be a group end
    }
}
