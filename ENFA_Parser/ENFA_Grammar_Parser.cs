﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_Grammar_Parser : ENFA_Parser
    {
        public ENFA_Grammar_Parser(ENFA_Controller controller) : base(controller)
        { }

        public override IEnumerable<List<ENFA_PatternMatch>> Parse(StreamReader streamReader)
        {
            throw new NotImplementedException();
        }
    }
}
