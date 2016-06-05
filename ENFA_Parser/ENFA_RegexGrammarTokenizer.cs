using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_RegexGrammarTokenizer : ENFA_GrammarTokenizer
    {


        public override IEnumerable<ENFA_Step> Tokenize(StreamReader reader)
        {
            if(reader.EndOfStream)
            {
                yield break;
            }
            else
            {
                yield return new ENFA_Step(null, new ENFA_StartingState(StateType.Transition));

                bool escaped = false;
                char newCharacter = (char) reader.Read();
                if(newCharacter == '\u005c')
                {
                    escaped = true;
                    if (reader.EndOfStream)
                    {
                        yield return new ENFA_Step(new ENFA_Transition(), new ENFA_State("Error", StateType.Error));
                    }
                    else
                    {
                        newCharacter = (char)reader.Read();
                    }
                }
                yield return new ENFA_Step(new ENFA_Transition(), new ENFA_State("Error",StateType.Error));
            }
        }
    }
}
