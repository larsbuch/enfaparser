using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_Grammar_Tokenizer : ENFA_Tokenizer
    {
        public ENFA_Grammar_Tokenizer(ENFA_Controller controller):base(controller)
        { }

        public override bool Tokenize(string ternimalName, StreamReader reader)
        {
            throw new NotImplementedException();
        }

    }
}
