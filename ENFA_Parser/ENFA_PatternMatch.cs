using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_PatternMatch
    {
        private string _patternMatched;
        private string _literalMatched;

        public ENFA_PatternMatch(string patternMatched, string literalMatched)
        {
            _patternMatched = patternMatched;
            _literalMatched = literalMatched;
        }

        public string PatternMatched
        {
            get
            {
                return _patternMatched;
            }
        }

        public string LiteralMatched
        {
            get
            {
                return _literalMatched;
            }
        }

    }
}
