using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_LookbehindEnd: ENFA_GroupingEnd
    {
        private ENFA_LookbehindStart _lookbehindStart;

        //A state can be a lookbehind end
        public ENFA_LookbehindEnd(ENFA_LookbehindStart lookbehindStart, ENFA_GroupingEnd parent) : base(parent)
        {
            _lookbehindStart = lookbehindStart;
        }

        public ENFA_LookbehindStart LookbehindStart
        {
            get
            {
                return _lookbehindStart;
            }
        }

        //public void Reverse()
        //{
        //    foreach(ENFA_Regex_Transition transition in LookbehindStart.GetTransitions)
        //    {
        //        AddTransition(transition);
        //        TraverseUntilLookbehindEndAndReplaceWithStart(transition);
        //    }
        //    LookbehindStart.ClearTransitions();
        //}

        //private void TraverseUntilLookbehindEndAndReplaceWithStart(ENFA_Regex_Transition transition)
        //{
        //    ENFA_Base state = transition.Transition();
        //    if(state is ENFA_LookbehindEnd)
        //    {
        //        /* replace state on transition */
        //        transition.ReplaceState(LookbehindStart);
        //    }
        //    else
        //    {
        //        foreach (ENFA_Regex_Transition subTransition in state.GetTransitions)
        //        {
        //            TraverseUntilLookbehindEndAndReplaceWithStart(transition);
        //        }
        //    }
        //}
    }
}
