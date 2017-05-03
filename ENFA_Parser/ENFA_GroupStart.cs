using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_GroupStart: ENFA_GroupingStart
    {
        public ENFA_GroupStart(ENFA_Controller controller, ENFA_GroupingStart parent) : base(controller, parent)
        {
        }
    }
}
