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

        //public IEnumerable<List<ENFA_Match>> NextMatches()
        //{
        //    List<ENFA_Match> matchList = Controller.Factory.CreateMatchList();
        //    List<ENFA_MatchPath> currentMatchPaths = Controller.Factory.CreateMatchPathList();
        //    while (currentMatchPaths.Count > 0)
        //    {
        //        matchList.Clear();
        //        foreach (ENFA_MatchPath matchPath in currentMatchPaths)
        //        {
        //            throw new NotImplementedException();
        //        }
        //        yield return matchList;
        //    }
        //}

        public override bool Parse(StreamReader streamReader)
        {
            bool exit = false;
            bool success = false;
            char? nextChar = NextCharInStream(streamReader);
            LinkedList<ENFA_Regex_MatchPath> currentMatchPaths = (Controller.Factory as ENFA_Regex_Factory).CreateMatchPathList();
            while (nextChar.HasValue && !exit)
            {
                foreach(ENFA_Regex_MatchPath matchPath in currentMatchPaths)
                {
                    List<ENFA_Regex_Match> validPaths = matchPath.ValidPaths(nextChar);
                    if (validPaths.Count == 0)
                    {
                        // Kill MatchPath
                        matchPath.Kill();
                        currentMatchPaths.Remove(matchPath);
                    }
                    else
                    {
                        // Split matchPath
                        for (int counter = 0; counter < validPaths.Count -1; counter += 1)
                        {
                            ENFA_Regex_MatchPath clone = matchPath.Clone();
                            clone.Transition(validPaths[counter]);
                            currentMatchPaths.AddBefore(matchPath.Node, clone.Node);
                        }
                        matchPath.Transition(validPaths[validPaths.Count - 1]);
                    }
                }
                nextChar = NextCharInStream(streamReader);
            }
            return success;
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
