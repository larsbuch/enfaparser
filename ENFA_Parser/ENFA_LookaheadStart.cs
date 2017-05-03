using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_LookaheadStart: ENFA_GroupingStart
    {
        private AssertionType _assertionType;

        //A state can be a lookahead start
        public ENFA_LookaheadStart(ENFA_Controller controller, AssertionType assertionType, ENFA_GroupingStart parent) : base(controller, parent)
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
