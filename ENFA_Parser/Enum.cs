using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public enum StateType
    {
        Negating,
        Accepting,
        Transition,
        Error
    }

    public enum ParserType
    {
        Regex,
        Language
    }

    public enum TransitionType
    {
        Literal,
        NegateLiteral,
        Letter,
        NegateLetter,
        Digit,
        NegateDigit,
        Whitespace,
        NegateWhitespace,
        Word,
        NegateWord,
        NewLine,
        NegateNewLine,
        WordBoundary,
        NegateWordBoundary,
        StartOfLine,
        EndOfLine,
        ExitState
    }
}
