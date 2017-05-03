using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_PatternStart: ENFA_GroupingStart
    {
        //A state can be a pattern start (always recording)
        public ENFA_PatternStart(ENFA_Controller controller) : base(controller,null)
        {
        }
    }
}
