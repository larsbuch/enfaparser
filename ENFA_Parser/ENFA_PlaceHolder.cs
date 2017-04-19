using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_PlaceHolder: ENFA_Base
    {
        private string _groupName;
        private int? _groupNumber;

        //A state* can be a placeholder for a recorded group
        public ENFA_PlaceHolder(string groupName) : base(StateType.NotApplicable)
        {
            _groupName = groupName;
        }
        public ENFA_PlaceHolder(int groupNumber) : base(StateType.NotApplicable)
        {
            _groupNumber = groupNumber;
        }

        public int? GroupNumber
        {
            get
            {
                return _groupNumber;
            }
        }

        public string GroupName
        {
            get
            {
                return _groupName;
            }
        }
    }
}
