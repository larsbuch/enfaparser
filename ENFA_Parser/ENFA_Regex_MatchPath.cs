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
        private ENFA_Controller _controller;
        private ENFA_Base _patternLocation;
        //        private ENFA_Regex_MatchTree _matchTree;
        private List<char> _patternMatched = new List<char>();

        public ENFA_Regex_MatchPath(ENFA_Controller controller)
        {
            if(controller.ParserType == ParserType.Grammar)
            {
                throw new ENFA_RegexRuntime_Exception(ErrorText.ControllerGrammarTypeInRegex);
            }
            _controller = controller;
            _patternLocation = _controller.PatternStart;
            _node = new LinkedListNode<ENFA_Regex_MatchPath>(this);
        }

        public ENFA_Regex_MatchPath(ENFA_Regex_MatchPath cloneThis)
        {
            _controller = cloneThis.Controller;
            _node = cloneThis.Node;
            _patternLocation = cloneThis.PatternLocation;
        }

        public LinkedListNode<ENFA_Regex_MatchPath> Node
        {
            get
            {
                return _node;
            }
        }

        internal ENFA_Controller Controller
        {
            get
            {
                return _controller;
            }
        }

        private ENFA_Base PatternLocation
        {
            get
            {
                return _patternLocation;
            }
            set
            {
                _patternLocation = value;
            }
        }

        public bool IsPatternMatch
        {
            get
            {
                return PatternLocation.StateType == StateType.Accepting;
            }
        }

        public string PatternMatched
        {
            get
            {
                return (PatternLocation as ENFA_PatternEnd).TerminalName;
            }
        }

        public string LiteralMatched
        {
            get
            {
                // TODO use structure
                StringBuilder builder = new StringBuilder();
                builder.Append(_patternMatched);
                return builder.ToString();
            }
        }

        public bool IsPatternEnd
        {
            get
            {
                switch (PatternLocation.StateType)
                {
                    case StateType.Accepting:
                        return true;
                    case StateType.Negating:
                        return true;
                    default:
                        return false;
                }
            }
        }

        private void Kill()
        {
            if (Node.List.Count > 1)
            {
                Node.List.Remove(this);

                // TODO remove matches from matchTree until split
                _controller = null;
                _node = null;
                _patternLocation = null;
            }
            else
            {
                // TODO Insert Error token for one char and go back
            }
        }

        private ENFA_Regex_MatchPath Clone()
        {
            return new ENFA_Regex_MatchPath(this);
        }

        internal void Transition(char? lastChar, char? nextChar)
        {
            List<ENFA_Regex_Match> validPaths = ValidPaths(lastChar, nextChar);
            if (validPaths.Count == 0)
            {
                // Kill MatchPath
                Kill();
            }
            else
            {
                // Split matchPath
                for (int counter = 0; counter < validPaths.Count - 1; counter += 1)
                {
                    ENFA_Regex_MatchPath clone = Clone();
                    Node.List.AddBefore(Node, clone.Node);
                    if (!clone.Transition(validPaths[counter]))
                    {
                        if (!clone.IsPatternEnd)
                        {
                            clone.Transition(lastChar, nextChar);
                        }
                    }
                }
                /* Transition once again if transition consumes no character */
                if (!Transition(validPaths[validPaths.Count - 1]))
                {
                    if (!IsPatternEnd)
                    {
                        Transition(lastChar, nextChar);
                    }
                }
            }
        }

        private bool Transition(ENFA_Regex_Match match)
        {
            PatternLocation = match.Transition.Transition();
            return match.ConsumesChar;
        }

        private List<ENFA_Regex_Match> ValidPaths(char? lastChar, char? nextChar)
        {
            List<ENFA_Regex_Match> returnList = new List<ENFA_Regex_Match>();
            bool consumesChar;
            foreach (ENFA_Regex_Transition transition in PatternLocation.GetTransitions)
            {
                if (transition.TransitionAllowed(lastChar, nextChar, out consumesChar))
                {
                    returnList.Add(new ENFA_Regex_Match(transition, consumesChar));
                }
            }
            return returnList;
        }

        public override string ToString()
        {
            return PatternLocation.StateName;
        }
    }
}
