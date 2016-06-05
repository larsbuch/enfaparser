using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_PlaceHolder: ENFA_Base
    {
        //A state* can be a placeholder for a recorded group
        public ENFA_PlaceHolder(StateType stateType) : base(stateType)
        { }
    }
}
