using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_LookbehindStart: ENFA_GroupingStart
    {
        private AssertionType _assertionType;

        //A state can be a lookbehind start
        public ENFA_LookbehindStart(AssertionType assertionType, ENFA_GroupingStart parent) : base(parent)
        {
            _assertionType = assertionType;
        }

        public AssertionType AssertionType
        {
            get
            {
                return _assertionType;
            }
        }
    }
}
