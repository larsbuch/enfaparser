using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_Language_Tokenizer : ENFA_Tokenizer
    {
        public ENFA_Language_Tokenizer(ENFA_Controller controller):base(controller)
        { }

        public override bool Tokenize(string ternimalName, StreamReader reader)
        {
            char? nextChar = NextCharInStream(reader);
            bool escaped = false;
            bool error = false;
            bool exit = false;
            ENFA_PatternStart _patternStart = new ENFA_PatternStart(ternimalName);
            ENFA_GroupingStart _parentStart = _patternStart;
            ENFA_GroupingEnd _parentEnd = new ENFA_PatternEnd(_parentStart as ENFA_PatternStart);
            ENFA_Base lastState = _parentStart;
            ENFA_Base nextState;
            ENFA_Regex_Transition activeTransition = null;
            while (nextChar.HasValue || !exit)
            {
                char? tempNextChar = PeekNextChar(reader);
                MatchingType matchingType = MatchingType.NotSet;
                if (!escaped)
                {
                    switch (nextChar.Value)
                    {
                        case Constants.Backslash:
                            escaped = true;
                            break;
                        case Constants.ExitContext:
                            exit = true;
                            break;
                        case Constants.StartOfLine:
                            nextState = new ENFA_State("Start of Line", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.StartOfLine, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case Constants.AllButNewLine:
                            nextState = new ENFA_State("Negate New Line", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.NegateNewLine, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case Constants.EndOfLine:
                            nextState = new ENFA_State("End of Line", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.EndOfLine, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case Constants.LeftCurlyBracket:
                            int minRepetitions;
                            int maxRepetitions;
                            CheckQuantifiers(reader, out minRepetitions, out maxRepetitions, out matchingType);
                            activeTransition.MinRepetitions = minRepetitions;
                            activeTransition.MaxRepetitions = maxRepetitions;
                            activeTransition.MatchingType = matchingType;
                            break;
                        case Constants.LeftSquareBracket:
                            if (tempNextChar.HasValue && tempNextChar.Value == Constants.CircumflexAccent)
                            {
                                nextState = new ENFA_State("Negate Character Group", StateType.Transition);
                                /* Remove CircumfelxAccent */
                                ConsumeNextChar(reader);
                                activeTransition = new ENFA_Regex_Transition(TransitionType.NegateLiteral, nextState);
                            }
                            else
                            {
                                nextState = new ENFA_State("Character Group", StateType.Transition);
                                activeTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            }
                            AddCharacterGroup(activeTransition, reader);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case Constants.LeftParanthesis:
                            bool recording = true;
                            string groupName = null;
                            if (tempNextChar.HasValue && tempNextChar.Value == Constants.QuestionMark)
                            {
                                /* Consume QuetionMark */
                                ConsumeNextChar(reader);
                                char? tempNextChar2 = PeekNextChar(reader);
                                if (tempNextChar2.Value == Constants.LessThanSign)
                                {
                                    /* Consume LessThanSign */
                                    ConsumeNextChar(reader);
                                    /* named group */
                                    groupName = GetGroupName(reader);
                                    _parentStart = new ENFA_GroupStart(recording, groupName, _parentStart);
                                    _parentEnd = new ENFA_GroupEnd(_parentStart as ENFA_GroupStart, _parentEnd);
                                }
                                else if (tempNextChar2.Value == Constants.Colon)
                                {
                                    /* Consume Colon */
                                    ConsumeNextChar(reader);
                                    /* non-recording group */
                                    recording = false;
                                    _parentStart = new ENFA_GroupStart(recording, groupName, _parentStart);
                                    _parentEnd = new ENFA_GroupEnd(_parentStart as ENFA_GroupStart, _parentEnd);
                                }
                                else if (tempNextChar2.Value == Constants.EqualsSign)
                                {
                                    /* Consume EqualSign */
                                    ConsumeNextChar(reader);
                                    /* positive lookahead */
                                    _parentStart = new ENFA_LookaheadStart(AssertionType.Positive, _parentStart);
                                    _parentEnd = new ENFA_LookaheadEnd(_parentStart as ENFA_LookaheadStart, _parentEnd);
                                }
                                else if (tempNextChar2.Value == Constants.ExclamationMark)
                                {
                                    /* Consume ExclamationMark */
                                    ConsumeNextChar(reader);
                                    /* negative lookahead */
                                    _parentStart = new ENFA_LookaheadStart(AssertionType.Negative, _parentStart);
                                    _parentEnd = new ENFA_LookaheadEnd(_parentStart as ENFA_LookaheadStart, _parentEnd);
                                }
                                else if (tempNextChar2.Value == Constants.LessThanSign)
                                {
                                    /* Consume ExclamationMark */
                                    ConsumeNextChar(reader);
                                    char? tempNextChar3 = PeekNextChar(reader);
                                    if (tempNextChar3.Value == Constants.EqualsSign)
                                    {
                                        /* positive lookbehind */
                                        _parentStart = new ENFA_LookbehindStart(AssertionType.Positive, _parentStart);
                                        _parentEnd = new ENFA_LookbehindEnd(_parentStart as ENFA_LookbehindStart, _parentEnd);
                                    }
                                    else if (tempNextChar3.Value == Constants.EqualsSign)
                                    {
                                        /* negative lookbehind */
                                        _parentStart = new ENFA_LookbehindStart(AssertionType.Negative, _parentStart);
                                        _parentEnd = new ENFA_LookbehindEnd(_parentStart as ENFA_LookbehindStart, _parentEnd);
                                    }
                                    else
                                    {
                                        /* Error */
                                    }
                                }
                            }
                            nextState = _parentStart;
                            activeTransition = new ENFA_Regex_Transition(TransitionType.GroupingStart, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case Constants.RightParanthesis:
                            nextState = _parentEnd;
                            activeTransition = new ENFA_Regex_Transition(TransitionType.GroupingEnd, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            _parentStart = _parentStart.Parent;
                            _parentEnd = _parentEnd.Parent;
                            break;
                        case Constants.VerticalLine:
                            nextState = _parentStart;
                            activeTransition = new ENFA_Regex_Transition(TransitionType.GroupingEnd, _parentEnd);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case Constants.PlusSign:
                            if (tempNextChar == Constants.QuestionMark)
                            {
                                /* Consume Quention Mark */
                                ConsumeNextChar(reader);
                                /* Lazy matching overwriting default */
                                matchingType = MatchingType.LazyMatching;
                            }
                            else if (tempNextChar == Constants.GreaterThanSign)
                            {
                                /* Consume Greater Than Sign */
                                ConsumeNextChar(reader);
                                /* Greedy matching overwriting default */
                                matchingType = MatchingType.GreedyMatching;
                            }
                            else
                            {
                                /* Use default matching */
                                matchingType = Controller.DefaultMatchType;
                            }
                            activeTransition.MinRepetitions = 1;
                            activeTransition.MaxRepetitions = -1;
                            activeTransition.MatchingType = matchingType;
                            break;
                        case Constants.Asterisk:
                            if (tempNextChar == Constants.QuestionMark)
                            {
                                /* Consume Quention Mark */
                                ConsumeNextChar(reader);
                                /* Lazy matching overwriting default */
                                matchingType = MatchingType.LazyMatching;
                            }
                            else if (tempNextChar == Constants.GreaterThanSign)
                            {
                                /* Consume Greater Than Sign */
                                ConsumeNextChar(reader);
                                /* Greedy matching overwriting default */
                                matchingType = MatchingType.GreedyMatching;
                            }
                            else
                            {
                                /* Use default matching */
                                matchingType = Controller.DefaultMatchType;
                            }
                            activeTransition.MinRepetitions = 0;
                            activeTransition.MaxRepetitions = -1;
                            activeTransition.MatchingType = matchingType;
                            break;
                        case Constants.QuestionMark:
                            if (tempNextChar == Constants.QuestionMark)
                            {
                                /* Consume Quention Mark */
                                ConsumeNextChar(reader);
                                /* Lazy matching overwriting default */
                                matchingType = MatchingType.LazyMatching;
                            }
                            else if (tempNextChar == Constants.GreaterThanSign)
                            {
                                /* Consume Greater Than Sign */
                                ConsumeNextChar(reader);
                                /* Greedy matching overwriting default */
                                matchingType = MatchingType.GreedyMatching;
                            }
                            else
                            {
                                /* Use default matching */
                                matchingType = Controller.DefaultMatchType;
                            }
                            activeTransition.MinRepetitions = 0;
                            activeTransition.MaxRepetitions = 1;
                            activeTransition.MatchingType = matchingType;
                            break;
                        default:
                            nextState = new ENFA_State(nextChar.Value, StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            activeTransition.AddLiteral(nextChar.Value);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                    }
                }
                else
                {
                    /* Escaped characters */
                    switch (nextChar.Value)
                    {
                        case '0':
                            nextState = new ENFA_State("Null Char", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            activeTransition.AddLiteral(Constants.NullChar);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'a':
                            nextState = new ENFA_State("Alert", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            activeTransition.AddLiteral(Constants.Alert);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'e':
                            nextState = new ENFA_State("Escape", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            activeTransition.AddLiteral(Constants.Escape);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'y':
                            nextState = new ENFA_State("Backspace", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            activeTransition.AddLiteral(Constants.Backspace);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'f':
                            nextState = new ENFA_State("Form Feed", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            activeTransition.AddLiteral(Constants.FormFeed);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'r':
                            nextState = new ENFA_State("Carriage Return", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            activeTransition.AddLiteral(Constants.CarriageReturn);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 't':
                            nextState = new ENFA_State("Horizontal Tab", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            activeTransition.AddLiteral(Constants.HorizontalTab);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'v':
                            nextState = new ENFA_State("Vertical Tab", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            activeTransition.AddLiteral(Constants.VerticalTab);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'n':
                            nextState = new ENFA_State("New Line", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.NewLine, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'w':
                            nextState = new ENFA_State("Word", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Word, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'W':
                            nextState = new ENFA_State("Negate Word", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.NegateWord, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'd':
                            nextState = new ENFA_State("Digit", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Digit, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'D':
                            nextState = new ENFA_State("Negate Digit", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.NegateDigit, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 's':
                            nextState = new ENFA_State("Whitespace", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Whitespace, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'S':
                            nextState = new ENFA_State("Negate Whitespace", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.NegateWhitespace, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'l':
                            nextState = new ENFA_State("Letter", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Letter, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'L':
                            nextState = new ENFA_State("Negate Letter", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.NegateLetter, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'b':
                            nextState = new ENFA_State("Word Boundary", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.WordBoundary, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'B':
                            nextState = new ENFA_State("Negate Word Boundary", StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.NegateWordBoundary, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            /* Back reference */
                            int groupNumber = int.Parse(nextChar.Value.ToString());
                            nextState = new ENFA_PlaceHolder(groupNumber);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.BackReference, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'k':
                            /* Named back reference like k<Bartho> */
                            string groupName = null;
                            if (tempNextChar.HasValue && tempNextChar.Value == Constants.LessThanSign)
                            {
                                /* Consume LessThanSign */
                                ConsumeNextChar(reader);
                                groupName = GetGroupName(reader);
                            }
                            else
                            {
                                // TODO Error
                            }
                            nextState = new ENFA_PlaceHolder(groupName);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.BackReference, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case Constants.DoubleQuote:
                        case Constants.LeftCurlyBracket:
                        case Constants.LeftSquareBracket:
                        case Constants.LeftParanthesis:
                        case Constants.RightParanthesis:
                        case Constants.VerticalLine:
                        case Constants.Backslash:
                        case Constants.FullStop:
                        case Constants.DollarSign:
                        case Constants.QuestionMark:
                        case Constants.PlusSign:
                        case Constants.Asterisk:
                        case Constants.CircumflexAccent:
                            nextState = new ENFA_State(nextChar.Value, StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            activeTransition.AddLiteral(nextChar.Value);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        default:
                            exit = true;
                            error = true;
                            break;
                    }
                    escaped = false;
                }
                if (!exit)
                {
                    nextChar = NextCharInStream(reader);
                }
            }
            return error;
        }

        private void CheckQuantifiers(StreamReader reader, out int minRepetitions, out int maxRepetitions, out MatchingType matchingType)
        {
            char? matchedChar;
            minRepetitions = 0;
            maxRepetitions = -1;
            /* Run digits until comma or curly barce end */
            string firstDigit = GetStringUntilChar(reader, new char[] { Constants.Comma, Constants.RightCurlyBracket }, out matchedChar);
            string secondDigit = null;
            if (!matchedChar.HasValue)
            {
                /* Error */
            }
            else
            {
                if (matchedChar.Value == Constants.RightCurlyBracket && firstDigit == String.Empty)
                {
                    /* Error */
                }
                else if (firstDigit != String.Empty)
                {
                    if (!int.TryParse(firstDigit, out minRepetitions))
                    {
                        /* Error */
                    }
                    else if (matchedChar.Value == Constants.RightCurlyBracket)
                    {
                        maxRepetitions = minRepetitions;
                    }
                    if (matchedChar.Value == Constants.Comma)
                    {
                        /* if comma is found then check digits until curly barce end */
                        secondDigit = GetStringUntilChar(reader, new char[] { Constants.RightCurlyBracket }, out matchedChar);
                        if (!matchedChar.HasValue)
                        {
                            /* Error */
                        }
                        else if (secondDigit == String.Empty)
                        {
                            maxRepetitions = -1;
                        }
                        else if (!int.TryParse(secondDigit, out maxRepetitions))
                        {
                            /* Error */
                        }
                    }
                }
            }
            /* Check for additional matching type */
            if (PeekNextChar(reader) == Constants.QuestionMark)
            {
                /* Consume Quention Mark */
                ConsumeNextChar(reader);
                /* Lazy matching overwriting default */
                matchingType = MatchingType.LazyMatching;
            }
            else if (PeekNextChar(reader) == Constants.GreaterThanSign)
            {
                /* Consume Greater Than Sign */
                ConsumeNextChar(reader);
                /* Greedy matching overwriting default */
                matchingType = MatchingType.GreedyMatching;
            }
            else
            {
                /* Use default matching */
                matchingType = Controller.DefaultMatchType;
            }
        }

        private string GetGroupName(StreamReader reader)
        {
            char? matchedChar;
            return GetStringUntilChar(reader, new char[] { Constants.GreaterThanSign }, out matchedChar);
        }

        private string GetStringUntilChar(StreamReader reader, char[] matchingChars, out char? matchedChar)
        {
            StringBuilder matchedString = new StringBuilder();
            bool exit = false;
            matchedChar = null;
            while (!exit)
            {
                char? nextChar = NextCharInStream(reader);
                if (!nextChar.HasValue)
                {
                    exit = true;
                }
                else
                {
                    foreach (char matchChar in matchingChars)
                    {
                        if (nextChar.Value == matchChar)
                        {
                            matchedChar = nextChar.Value;
                            exit = true;
                        }
                    }
                    if (!exit)
                    {
                        matchedString.Append(nextChar.Value);
                    }
                }
            }
            return matchedString.ToString();
        }

        private void AddCharacterGroup(ENFA_Regex_Transition activeTransition, StreamReader reader)
        {
            bool exit = false;
            while (!exit)
            {
                char? nextChar = NextCharInStream(reader);
                if (!nextChar.HasValue)
                {
                    exit = true;
                }
                else
                {
                    if (nextChar.Value == Constants.RightSquareBracket)
                    {
                        exit = true;
                    }
                    else
                    {
                        if (PeekNextChar(reader).HasValue && PeekNextChar(reader).Value == Constants.HyphenMinusSign)
                        {
                            /* Char range */
                            /* Consume Hyphen Minus Sign */
                            ConsumeNextChar(reader);
                            char? endRange = NextCharInStream(reader);
                            if (!endRange.HasValue)
                            {
                                /* Error */
                            }
                            int lowCounter = (int)nextChar.Value;
                            int highCounter = (int)endRange.Value;
                            if (lowCounter > highCounter)
                            {
                                int temp = lowCounter;
                                lowCounter = highCounter;
                                highCounter = temp;
                            }
                            for (int counter = lowCounter; counter <= highCounter; counter += 1)
                            {
                                activeTransition.AddLiteral((char)counter);
                            }
                        }
                        else
                        {
                            activeTransition.AddLiteral(nextChar.Value);
                        }
                    }
                }
            }
        }

        private char? PeekNextChar(StreamReader reader)
        {
            return (char?)reader.Peek();
        }

        private void ConsumeNextChar(StreamReader reader)
        {
            NextCharInStream(reader);
        }

        private char? NextCharInStream(StreamReader reader)
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
