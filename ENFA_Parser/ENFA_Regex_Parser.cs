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

        public override IEnumerable<List<ENFA_PatternMatch>> Parse(StreamReader streamReader)
        {
            bool exit = false;
            char? nextChar = NextCharInStream(streamReader);
            char? lastChar = null;
            LinkedList<ENFA_Regex_MatchPath> currentMatchPaths = (Controller.Factory as ENFA_Regex_Factory).CreateMatchPathList();
            List<ENFA_PatternMatch> patternMatches = (Controller.Factory as ENFA_Regex_Factory).CreatePatternMatchList();
            while (!exit)
            {
                if (!nextChar.HasValue)
                {
                    exit = true;
                }
                foreach (ENFA_Regex_MatchPath matchPath in currentMatchPaths)
                {
                    matchPath.Transition(lastChar, nextChar);
                    if (matchPath.IsPatternMatch)
                    {
                        patternMatches.Add(new ENFA_PatternMatch(matchPath.PatternMatched, matchPath.LiteralMatched));
                    }
                }
                if (patternMatches.Count > 0)
                {
                    yield return patternMatches;
                    patternMatches = (Controller.Factory as ENFA_Regex_Factory).CreatePatternMatchList();
                }
                if (!exit)
                {
                    lastChar = nextChar;
                    nextChar = NextCharInStream(streamReader);
                }
            }
        }

        private char? PeekNextChar(StreamReader reader)
        {
            return (char?)reader.Peek();
        }

        private void ConsumeNextChar(StreamReader reader)
        {
            NextCharInStream(reader);
        }

        private char? NextCharInStream(StreamReader reader)
        {
            if (reader.EndOfStream)
            {
                return null;
            }
            else
            {
                char next = (char)reader.Read();
                return next;
            }
        }
    }
}
