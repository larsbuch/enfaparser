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

        private RegexTransitionType _transitionType;
        private List<char> _literal;

        public ENFA_Regex_Transition(RegexTransitionType transitionType, ENFA_Base nextState):base(nextState)
        {
            _transitionType = transitionType;
            _literal = new List<char>();
        }

        public void AddLiteral(char literal)
        {
            if (!_literal.Contains(literal))
            {
                _literal.Add(literal);
                _literal.Sort();
            }
        }

        public bool TransitionAllowed(char? lastChar, char nextChar, out bool consumesChar)
        {
            switch(_transitionType)
            {
                case RegexTransitionType.Literal:
                    consumesChar = true;
                    return _literal.Contains(nextChar);
                case RegexTransitionType.NegateLiteral:
                    consumesChar = true;
                    return ! _literal.Contains(nextChar);
                case RegexTransitionType.Letter:
                    consumesChar = true;
                    return Char.IsLetter(nextChar);
                case RegexTransitionType.NegateLetter:
                    consumesChar = true;
                    return !Char.IsLetter(nextChar);
                case RegexTransitionType.Digit:
                    consumesChar = true;
                    return Char.IsDigit(nextChar);
                case RegexTransitionType.NegateDigit:
                    consumesChar = true;
                    return !Char.IsDigit(nextChar);
                case RegexTransitionType.NewLine:
                    consumesChar = true;
                    return nextChar == Constants.NewLine;
                case RegexTransitionType.NegateNewLine:
                    consumesChar = true;
                    return nextChar != Constants.NewLine;
                case RegexTransitionType.Whitespace:
                    consumesChar = true;
                    return Char.IsWhiteSpace(nextChar);
                case RegexTransitionType.NegateWhitespace:
                    consumesChar = true;
                    return !Char.IsWhiteSpace(nextChar);
                case RegexTransitionType.Word:
                    consumesChar = true;
                    return WordChar(nextChar);
                case RegexTransitionType.NegateWord:
                    consumesChar = true;
                    return !WordChar(nextChar);
                case RegexTransitionType.WordBoundary:
                    consumesChar = false;
                    return WordBoundary(lastChar, nextChar);
                case RegexTransitionType.NegateWordBoundary:
                    consumesChar = false;
                    return !WordBoundary(lastChar, nextChar);
                case RegexTransitionType.StartOfLine:
                    consumesChar = false;
                    if( !lastChar.HasValue || (lastChar.HasValue && lastChar.Value == Constants.NewLine)) 
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case RegexTransitionType.EndOfLine:
                    consumesChar = false;
                    if(nextChar == Constants.NewLine)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case RegexTransitionType.ExitContext:
                    consumesChar = true;
                    return true;
                default:
                    throw new ENFA_Exception("Transition Type has not been defined");
            }
        }

        public RegexTransitionType TransitionType
        {
            get
            {
                return _transitionType;
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

        public override string ToString()
        {
            return TransitionType.ToString();
        }
    }
}
