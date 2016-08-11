using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_Regex_Tokenizer : ENFA_Tokenizer
    {

        public override bool Tokenize(ENFA_StartingState startingState, string nonTernimalName, StreamReader reader)
        {
            char? nextChar = nextCharInStream(reader);
            bool escaped = false;
            bool error = false;
            bool exit = false;
            Stack<ENFA_Base> _parentStack = new Stack<ENFA_Base>();
            _parentStack.Push(startingState);
            ENFA_Base lastState = startingState;
            ENFA_Regex_Transition nextTransition;
            while (nextChar != null || exit)
            {
                if (!escaped)
                {
                    switch (nextChar)
                    {
                        case Constants.Backslash:
                            escaped = true;
                            break;
                        case Constants.ExitContext:
                            if ((char)reader.Peek() != Constants.DoubleQuote)
                            {
                                // Non escaped ExitContext
                                exit = true;
                            }
                            else
                            {
                                escaped = true;
                            }
                            break;
                        //case Constants.StartOfLine:


                    }
                }
                else
                {
                    /* Escaped characters */
                    switch (nextChar)
                    {
                        case Constants.DoubleQuote:
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Literal, new ENFA_State(nextChar.Value, StateType.Transition));
                            nextTransition.AddLiteral(Constants.DoubleQuote);
                            lastState.AddTransition(nextTransition);
                            break;
                    }
                }
                if (!exit)
                {
                    nextChar = nextCharInStream(reader);
                }
            }
            return error;
        }

        private char? nextCharInStream(StreamReader reader)
        {
            if (reader.EndOfStream)
            {
                return null;
            }
            else
            {
                return (char)reader.Read();
            }
        }
    }
}
