using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public abstract class ENFA_GrammarTokenizer
    {
        public ENFA_GrammarTokenizer()
        {
        }

        public abstract IEnumerable<ENFA_Step> Tokenize(StreamReader streamReader);
    }
}
