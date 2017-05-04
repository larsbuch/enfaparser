using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public abstract class ENFA_Base
    {
        //A state can be a negating state which terminates the state-visitor path
        //A state can be an accepting state which is a match
        //A state can be a transition state(not negating or accepting)

        private StateType _stateType;
        protected List<ENFA_Transition> _nextTransitions;
        private ENFA_Controller _controller;

        public ENFA_Base(ENFA_Controller controller, StateType stateType)
        {
            _controller = controller;
            _stateType = stateType;
            _nextTransitions = new List<ENFA_Transition>();
        }

        public StateType StateType
        {
            get
            {
                return _stateType;
            }
        }

        public virtual string StateName
        {
            get
            {
                return GetType().Name;
            }
        }

        public ENFA_Controller Controller
        {
            get
            {
                return _controller;
            }
        }

        public void AddTransition(ENFA_Transition nextTransition)
        {
            _nextTransitions.Add(nextTransition);
        }

        public List<ENFA_Transition> GetTransitions
        {
            get
            {
                return _nextTransitions;
            }
        }

        internal virtual ENFA_Transition NewGrammarTransition(GrammarTransitionType transitionType, ENFA_Base nextState)
        {
            if (Controller.ParserType == ParserType.Regex)
            {
                throw new NotImplementedException();

                //                throw new ENFA_RegexBuild_Exception();
            }
            throw new NotImplementedException();
            /* Look through transitions for TransitionType and NextState and if none are matching create a new otherwise return the existing */
            return (Controller.Factory as ENFA_Grammar_Factory).CreateGrammarTransition(transitionType, nextState);
        }

        internal virtual ENFA_Transition NewRegexTransition(RegexTransitionType transitionType, ENFA_Base nextState)
        {
            if(Controller.ParserType == ParserType.Grammar)
            {
                throw new NotImplementedException();

//                throw new ENFA_GrammarBuild_Exception();
            }
            throw new NotImplementedException();
            /* Look through transitions for TransitionType and NextState and if none are matching create a new otherwise return the existing */
            return (Controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(transitionType, nextState);
        }

        internal virtual ENFA_Base NewState(char stateName, StateType stateType)
        {
            return NewState(stateName.ToString(), stateType);
        }

        internal virtual ENFA_Base NewState(string stateName, StateType stateType)
        {
            throw new NotImplementedException();
            /* Look through transitions for NextState and if none are matching create a new otherwise return the existing */
            return Controller.Factory.CreateState(stateName, stateType);
        }

        internal virtual ENFA_Base NewPlaceHolder(string groupName)
        {
            return Controller.Factory.CreatePlaceHolder(groupName);
        }
    }
}
