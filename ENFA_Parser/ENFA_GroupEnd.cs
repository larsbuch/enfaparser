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

        //A state can be a group end
        public ENFA_GroupEnd(ENFA_GroupStart groupStart, bool recording, string groupName, ENFA_GroupingEnd parent) :base(recording,groupName,parent)
        {
            _groupStart = groupStart;
        }

        public ENFA_GroupStart GroupStart
        {
            get
            {
                return _groupStart;
            }
        }
    }
}
