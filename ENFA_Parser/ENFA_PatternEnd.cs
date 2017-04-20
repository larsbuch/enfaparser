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

        public ENFA_PatternEnd(ENFA_PatternStart patternStart) :base(null)
        {
            _patternStart = patternStart;
        }

        public ENFA_PatternStart PatternStart
        {
            get
            {
                return _patternStart;
            }
        }
        //A state can be a pattern end
    }
}
