using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_Regex_Factory : ENFA_Factory
    {
        private ENFA_Regex_Tokenizer _regexTokenizer;
        private ENFA_Parser _regexParser;

        public ENFA_Regex_Factory(ENFA_Controller controller) : base(controller)
        {
            _regexTokenizer = new ENFA_Regex_Tokenizer(controller);
            _regexParser = new ENFA_Regex_Parser(controller);
        }

        public override ENFA_Tokenizer GetTokenizer()
        {
            return _regexTokenizer;
        }

        internal override ENFA_Parser GetParser()
        {
            return _regexParser;
        }

        internal override ENFA_GroupingEnd CreateGroupEnd(ENFA_GroupStart groupStart, bool recording, string groupName, ENFA_GroupingEnd parentEnd)
        {
            return new ENFA_GroupEnd(Controller, groupStart, recording, groupName, parentEnd);
        }

        internal override ENFA_GroupingStart CreateGroupStart(ENFA_GroupingStart parentStart)
        {
            return new ENFA_GroupStart(Controller, parentStart);
        }

        internal override ENFA_GroupingEnd CreateLookaheadEnd(ENFA_LookaheadStart lookaheadStart, ENFA_GroupingEnd parentEnd)
        {
            return new ENFA_LookaheadEnd(Controller, lookaheadStart, parentEnd);
        }

        internal override ENFA_GroupingStart CreateLookaheadStart(AssertionType positive, ENFA_GroupingStart parentStart)
        {
            return new ENFA_LookaheadStart(Controller, positive, parentStart);
        }

        internal override ENFA_GroupingEnd CreateLookbehindEnd(ENFA_LookbehindStart lookbehindStart, ENFA_GroupingEnd parentEnd)
        {
            return new ENFA_LookbehindEnd(Controller, lookbehindStart, parentEnd);
        }

        internal override ENFA_GroupingStart CreateLookbehindStart(AssertionType positive, ENFA_GroupingStart parentStart)
        {
            return new ENFA_LookbehindStart(Controller, positive, parentStart);
        }

        internal override ENFA_PatternEnd CreatePatternEnd(ENFA_PatternStart patternStart, string terminalName)
        {
            return new ENFA_PatternEnd(patternStart, Controller, terminalName);
        }

        internal override ENFA_Base CreatePlaceHolder(string groupName)
        {
            return new ENFA_PlaceHolder(Controller, groupName);
        }

        internal override ENFA_Base CreateState(string stateName, StateType stateType)
        {
            return new ENFA_State(Controller, stateName, stateType);
        }

        internal override ENFA_Transition CreateTransition(TransitionType transitionType, ENFA_Base nextState)
        {
            return new ENFA_Regex_Transition(transitionType, nextState);
        }
    }
}
