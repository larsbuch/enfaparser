using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public abstract class ENFA_GroupingEnd:ENFA_Base
    {
        private ENFA_GroupingEnd _parent;
        private bool _recording;
        private string _groupName;

        public ENFA_GroupingEnd(ENFA_Controller controller, bool recording, string groupName, ENFA_GroupingEnd parent):base(controller, StateType.NotApplicable)
        {
            _parent = parent;
            _recording = recording;
            if(string.IsNullOrEmpty(groupName))
            {
                groupName = Guid.NewGuid().ToString();
            }
            _groupName = groupName;
            if (_recording)
            {
                RegisterGroupName(_groupName);
            }
        }

        internal abstract void RegisterGroupName(string groupName);

        public ENFA_GroupingEnd Parent
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
