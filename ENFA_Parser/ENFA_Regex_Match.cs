using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_Regex_Match : ENFA_Match
    {
        private ENFA_Regex_Transition _transition;
        private bool _consumesChar;

        public ENFA_Regex_Match(ENFA_Regex_Transition transition, bool consumesChar)
        {
            _transition = transition;
            _consumesChar = consumesChar;
        }

        public ENFA_Regex_Transition Transition
        {
            get
            {
                return _transition;
            }
        }

        public bool ConsumesChar
        {
            get
            {
                return _consumesChar;
            }
        }
    }
}
