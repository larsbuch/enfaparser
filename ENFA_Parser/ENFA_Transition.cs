using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_Transition
    {
        //        A state transition can contain
        //- a counter max or min(* has min 0 and max -1, + has min 1 and max -1, ? has min 0 and max 1, { x,y}
        //        has min x and max y with x omitted has min 0 with y omitted has max -1, {x
        //    }
        //    has max = x and min = x)
        //- a char number range*
        //- a char type word, digit etc*
        //- a boundry such as word boundry*
        //- a scope identifier#
        //- a non-terminal#
        //- a terminal# matched on regex level
        // contextTransition
    }
}
