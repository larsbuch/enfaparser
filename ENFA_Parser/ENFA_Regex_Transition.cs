using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_Regex_Transition:ENFA_Transition
    {
        //        A state transition can contain
        //- a counter max or min(* has min 0 and max -1, + has min 1 and max -1, ? has min 0 and max 1, { x,y}
        //        has min x and max y with x omitted has min 0 with y omitted has max -1, {x
        //    }
        //    has max = x and min = x)
        //Done: - a char number range* (tokenizer adds literals for all)
        //Done: - a char type word, digit etc*
        //Done: - a boundry such as word boundry*
        // contextTransition

        private TransitionType _transitionType;
        private List<char> _literal;
        private ENFA_Base _nextState;

        public ENFA_Regex_Transition(TransitionType transitionType, ENFA_Base nextState)
        {
            _transitionType = transitionType;
            _literal = new List<char>();
            _nextState = nextState;
        }

        public void AddLiteral(char literal)
        {
            _literal.Add(literal);
        }

        public ENFA_Base Transition()
        {
            return _nextState;
        }

        public bool TransitionAllowed(char? lastChar, char nextChar, out bool consumesChar)
        {
            switch(_transitionType)
            {
                case TransitionType.Literal:
                    consumesChar = true;
                    return _literal.Contains(nextChar);
                case TransitionType.NegateLiteral:
                    consumesChar = true;
                    return ! _literal.Contains(nextChar);
                case TransitionType.Letter:
                    consumesChar = true;
                    return Char.IsLetter(nextChar);
                case TransitionType.NegateLetter:
                    consumesChar = true;
                    return !Char.IsLetter(nextChar);
                case TransitionType.Digit:
                    consumesChar = true;
                    return Char.IsDigit(nextChar);
                case TransitionType.NegateDigit:
                    consumesChar = true;
                    return !Char.IsDigit(nextChar);
                case TransitionType.NewLine:
                    consumesChar = true;
                    return nextChar == Constants.NewLine;
                case TransitionType.NegateNewLine:
                    consumesChar = true;
                    return nextChar != Constants.NewLine;
                case TransitionType.Whitespace:
                    consumesChar = true;
                    return Char.IsWhiteSpace(nextChar);
                case TransitionType.NegateWhitespace:
                    consumesChar = true;
                    return !Char.IsWhiteSpace(nextChar);
                case TransitionType.Word:
                    consumesChar = true;
                    return WordChar(nextChar);
                case TransitionType.NegateWord:
                    consumesChar = true;
                    return !WordChar(nextChar);
                case TransitionType.WordBoundary:
                    consumesChar = false;
                    return WordBoundary(lastChar, nextChar);
                case TransitionType.NegateWordBoundary:
                    consumesChar = false;
                    return !WordBoundary(lastChar, nextChar);
                case TransitionType.StartOfLine:
                    consumesChar = false;
                    if( !lastChar.HasValue || (lastChar.HasValue && lastChar.Value == Constants.NewLine)) 
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case TransitionType.EndOfLine:
                    consumesChar = false;
                    if(nextChar == Constants.NewLine)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case TransitionType.ExitState:
                    consumesChar = true;
                    return true;
                default:
                    throw new ENFA_Exception("Transition Type has not been defined");
            }
        }

        private bool WordChar(char? testChar)
        {
            if (testChar.HasValue)
            {
                if (Char.IsLetterOrDigit(testChar.Value) || testChar.Value == Constants.Underscore)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool WordChar(char testChar)
        {
                if (Char.IsLetterOrDigit(testChar) || testChar == Constants.Underscore)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        private bool WordBoundary(char? lastChar, char nextChar)
        {
            if((WordChar(lastChar) && WordChar(nextChar)) || (!WordChar(lastChar) && !WordChar(nextChar)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
