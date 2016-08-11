using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENFA_Parser
{
    public class ENFA_State:ENFA_Base
    {
        private string _stateName;

        public ENFA_State(char stateName, StateType stateType) : this(stateName.ToString(),stateType)
        { }

        public ENFA_State(string stateName, StateType stateType):base(stateType)
        {
            _stateName = stateName;
        }

        public override string StateName
        {
            get
            {
                return _stateName;
            }
        }
    }
}
