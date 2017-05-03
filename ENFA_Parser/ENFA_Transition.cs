using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public abstract class ENFA_Transition
    {
        //        A state transition can contain
        //- a counter max or min(* has min 0 and max -1, + has min 1 and max -1, ? has min 0 and max 1, { x,y}
        //        has min x and max y with x omitted has min 0 with y omitted has max -1, {x
        //    }
        //    has max = x and min = x)
        //- a scope identifier#
        //- a non-terminal#
        //- a terminal# matched on regex level
        // contextTransition
        private ENFA_Base _nextState;
        private int _minRepetitions;
        private int _maxRepetitions;
        private MatchingType _matchingType;

        public ENFA_Transition(ENFA_Base nextState)
        {
            _nextState = nextState;
            _minRepetitions = 1;
            _maxRepetitions = 1;
            _matchingType = MatchingType.NotSet;
        }

        public int MinRepetitions
        {
            get
            {
                return _minRepetitions;
            }
            set
            {
                _minRepetitions = value;
            }
        }

        public int MaxRepetitions
        {
            get
            {
                return _maxRepetitions;
            }
            set
            {
                _maxRepetitions = value;
            }
        }

        public MatchingType MatchingType
        {
            get
            {
                return _matchingType;
            }
            set
            {
                _matchingType = value;
            }
        }
        public ENFA_Base Transition()
        {
            return _nextState;
        }
    }
}
