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

        //A state can be a pattern end
        public ENFA_PatternEnd(ENFA_PatternStart patternStart, string terminalName) :base(null)
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

    }
}
