using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_Regex_MatchPath : ENFA_MatchPath
    {
        private LinkedListNode<ENFA_Regex_MatchPath> _node;

        public ENFA_Regex_MatchPath()
        {
            _node = new LinkedListNode<ENFA_Regex_MatchPath>(this);
        }

        public LinkedListNode<ENFA_Regex_MatchPath> Node
        {
            get
            {
                return _node;
            }
        }

        internal void Kill()
        {
            throw new NotImplementedException();
        }

        internal ENFA_Regex_MatchPath Clone()
        {
            throw new NotImplementedException();
        }

        internal void Transition(ENFA_Regex_Match match)
        {
            throw new NotImplementedException();
        }

        internal List<ENFA_Regex_Match> ValidPaths(char? nextChar)
        {
            throw new NotImplementedException();
        }
    }
}
