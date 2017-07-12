using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_PatternEnd: ENFA_GroupingEnd
    {
        private ENFA_PatternStart _patternStart;
        private string _terminalName;
        private List<string> _groupNames = new List<string>();

        //A state can be a pattern end
        public ENFA_PatternEnd(ENFA_PatternStart patternStart, ENFA_Controller controller, string terminalName, StateType stateType) :base(controller, true, terminalName, null, stateType)
        {
            _patternStart = patternStart;
            _terminalName = terminalName;
        }

        public ENFA_PatternStart PatternStart
        {
            get
            {
                return _patternStart;
            }
        }

        public string TerminalName
        {
            get
            {
                return _terminalName;
            }
        }

        internal override void RegisterGroupName(string groupName)
        {
            _groupNames.Add(groupName);
        }

        public string LookupGroupNameFromNumber(int number)
        {
            if(number > _groupNames.Count + 1)
            {
                throw new ENFA_RegexBuild_Exception(TerminalName,"Unknown",ErrorText.LookupGroupNameFromNumberTooHighNumber);
            }
            return _groupNames[number];
        }

        public bool GroupNameExists(string groupName)
        {
            return _groupNames.Contains(groupName);
        }

        public override string ToString()
        {
            return string.Format("Terminal Match [{0}]", TerminalName);
        }
    }
}
