using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_GroupStart: ENFA_GroupingStart
    {
        private bool _recording;
        private string _groupName;

        public ENFA_GroupStart(bool recording, string groupName, ENFA_GroupingStart parent) : base(parent)
        {
            _recording = recording;
            _groupName = groupName;
        }

        public bool Recording
        {
            get
            {
                return _recording;
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
