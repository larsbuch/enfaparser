using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_LookbehindEnd: ENFA_GroupingEnd
    {
        private ENFA_LookbehindStart _lookbehindStart;

        //A state can be a lookbehind end
        public ENFA_LookbehindEnd(ENFA_LookbehindStart lookbehindStart, ENFA_GroupingEnd parent) : base(false, null, parent)
        {
            _lookbehindStart = lookbehindStart;
        }

        public ENFA_LookbehindStart LookbehindStart
        {
            get
            {
                return _lookbehindStart;
            }
        }
    }
}
