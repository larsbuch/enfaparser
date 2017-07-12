using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_Grammar_Factory : ENFA_Factory
    {
        private ENFA_Grammar_Tokenizer _grammarTokenizer;
        private ENFA_Parser _grammarParser;

        public ENFA_Grammar_Factory(ENFA_Controller controller) : base(controller)
        {
            _grammarTokenizer = new ENFA_Grammar_Tokenizer(controller);
            _grammarParser = new ENFA_Grammar_Parser(controller);
        }

        public override ENFA_Tokenizer GetTokenizer()
        {
            return _grammarTokenizer;
        }

        public override ENFA_Parser GetParser()
        {
            return _grammarParser;
        }

        public override ENFA_GroupingEnd CreateGroupEnd(ENFA_GroupStart groupStart, bool recording, string groupName, ENFA_GroupingEnd parentEnd)
        {
            return new ENFA_GroupEnd(Controller, groupStart, recording, groupName, parentEnd);
        }

        public override ENFA_GroupingStart CreateGroupStart(ENFA_GroupingStart parentStart)
        {
            return new ENFA_GroupStart(Controller, parentStart);
        }

        public override ENFA_GroupingEnd CreateLookaheadEnd(ENFA_LookaheadStart lookaheadStart, ENFA_GroupingEnd parentEnd)
        {
            return new ENFA_LookaheadEnd(Controller, lookaheadStart, parentEnd);
        }

        public override ENFA_GroupingStart CreateLookaheadStart(AssertionType positive, ENFA_GroupingStart parentStart)
        {
            return new ENFA_LookaheadStart(Controller, positive, parentStart);
        }

        public override ENFA_GroupingEnd CreateLookbehindEnd(ENFA_LookbehindStart lookbehindStart, ENFA_GroupingEnd parentEnd)
        {
            return new ENFA_LookbehindEnd(Controller, lookbehindStart, parentEnd);
        }

        public override ENFA_GroupingStart CreateLookbehindStart(AssertionType positive, ENFA_GroupingStart parentStart)
        {
            return new ENFA_LookbehindStart(Controller, positive, parentStart);
        }

        public override ENFA_PatternEnd CreatePatternEnd(ENFA_PatternStart patternStart, string terminalName, StateType stateType)
        {
            return new ENFA_PatternEnd(patternStart, Controller, terminalName, stateType);
        }

        public override ENFA_Base CreatePlaceHolder(string groupName)
        {
            return new ENFA_PlaceHolder(Controller, groupName);
        }

        public override ENFA_Base CreateState(ENFA_Base prevoiusState, string stateName, StateType stateType)
        {
            return new ENFA_State(Controller, prevoiusState, stateName, stateType);
        }

        public ENFA_Grammar_Transition CreateGrammarTransition(GrammarTransitionType transitionType, ENFA_Base nextState)
        {
            return new ENFA_Grammar_Transition(transitionType, nextState);
        }
    }
}
