using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_GroupStart: ENFA_GroupingStart
    {
        private ENFA_GroupingStart _parent;
        private bool _recording;
        private string _groupName;

        //A state can be a group start (recording* or non recording)
        public ENFA_GroupStart(ENFA_GroupingStart parent, bool recording, string groupName) : base()
        {
            _parent = parent;
            _recording = recording;
            _groupName = groupName;
        }

        public ENFA_GroupingStart Parent
        {
            get
            {
                return _parent;
            }
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
