using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_PatternStart: ENFA_GroupingStart
    {
        private string _terminalName;
        //A state can be a pattern start (always recording)
        public ENFA_PatternStart(string terminalName) : base()
        {
            _terminalName = terminalName;
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
