using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_Regex_Parser:ENFA_Parser
    {
        public ENFA_Regex_Parser(ENFA_Controller controller):base(controller)
        { }

        public IEnumerable<List<ENFA_Match>> NextMatches()
        {
            List<ENFA_Match> matchList = Controller.Factory.CreateMatchList();
            List<ENFA_MatchPath> currentMatchPaths = Controller.Factory.CreateMatchPathList();
            while (currentMatchPaths.Count > 0)
            {
                matchList.Clear();
                foreach (ENFA_MatchPath lexerPath in currentMatchPaths)
                {
                    NextTokens(lexerPath, matchList);
                }
                yield return matchList;
            }
        }
    }
}
