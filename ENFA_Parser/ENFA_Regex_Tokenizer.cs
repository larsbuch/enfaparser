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
        private List<char> _matchedCharInRegexBuild;
        private string _currectTerminalName;

        public ENFA_Regex_Tokenizer(ENFA_Controller controller):base(controller)
        { }

        public override bool Tokenize(string terminalName, StreamReader reader)
        {
            _matchedCharInRegexBuild = new List<char>();
            _currectTerminalName = terminalName;
            char? nextChar = NextCharInStream(reader);
            bool escaped = false;
            bool success = false; // Error until proven correct
            bool exit = false;
            ENFA_GroupingStart _parentStart = Controller.PatternStart;
            ENFA_PatternEnd _patternEnd = new ENFA_PatternEnd(_parentStart as ENFA_PatternStart, terminalName);
            ENFA_GroupingEnd _parentEnd = _patternEnd;
            ENFA_Base lastState = _parentStart;
            ENFA_Base nextState;
            ENFA_Regex_Transition activeTransition = null;
            while (nextChar.HasValue && !exit)
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
                            activeTransition = new ENFA_Regex_Transition(TransitionType.GroupingEnd, _parentEnd);
                            lastState.AddTransition(activeTransition);
                            exit = true;
                            if (_parentEnd is ENFA_PatternEnd)
                            {
                                success = true;
                            }
                            else
                            {
                                ThrowBuildException(ErrorText.ExitContextBeforePatternEnd);
                            }
                            break;
                        case Constants.StartOfLine:
                            nextState = new ENFA_State(TransitionType.StartOfLine.ToString(), StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.StartOfLine, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case Constants.AllButNewLine:
                            nextState = new ENFA_State(TransitionType.NegateNewLine.ToString(), StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.NegateNewLine, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case Constants.EndOfLine:
                            if (lastState is ENFA_PatternStart)
                            {
                                ThrowBuildException(ErrorText.EndOfLineAsFirstCharInPattern);
                            }
                            nextState = new ENFA_State(TransitionType.EndOfLine.ToString(), StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.EndOfLine, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case Constants.RightCurlyBracket:
                            ThrowBuildException(ErrorText.RightCurlyBracketWithoutMatchingLeftCurlyBracket);
                            break;
                        case Constants.LeftCurlyBracket:
                            if (lastState is ENFA_PatternStart)
                            {
                                ThrowBuildException(ErrorText.LeftCurlyBracketAsFirstCharInPattern);
                            }
                            int minRepetitions;
                            int maxRepetitions;
                            CheckQuantifiers(reader, out minRepetitions, out maxRepetitions, out matchingType);
                            activeTransition.MinRepetitions = minRepetitions;
                            activeTransition.MaxRepetitions = maxRepetitions;
                            activeTransition.MatchingType = matchingType;
                            break;
                        case Constants.RightSquareBracket:
                            ThrowBuildException(ErrorText.RightSquareBracketWithoutMatchingLeftSquareBracket);
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
                        case Constants.GroupingStart:
                            bool recording = Controller.DefaultGroupingRecording;
                            string groupName = null;
                            if (tempNextChar.HasValue && tempNextChar.Value == Constants.QuestionMark)
                            {
                                /* Consume QuetionMark */
                                ConsumeNextChar(reader);
                                char? tempNextChar2 = PeekNextChar(reader);
                                switch (tempNextChar2.Value)
                                {
                                    case Constants.Colon:
                                        /* Consume Colon */
                                        ConsumeNextChar(reader);
                                        /* non-recording group */
                                        recording = false;
                                        _parentStart = new ENFA_GroupStart(recording, groupName, _parentStart);
                                        _parentEnd = new ENFA_GroupEnd(_parentStart as ENFA_GroupStart, _parentEnd);
                                        break;
                                    case Constants.GreaterThanSign:
                                        /* Consume Greater Than Sign */
                                        ConsumeNextChar(reader);
                                        /* recording group */
                                        recording = true;
                                        _parentStart = new ENFA_GroupStart(recording, groupName, _parentStart);
                                        _parentEnd = new ENFA_GroupEnd(_parentStart as ENFA_GroupStart, _parentEnd);
                                        break;
                                    case Constants.EqualsSign:
                                        /* Consume EqualSign */
                                        ConsumeNextChar(reader);
                                        /* positive lookahead */
                                        _parentStart = new ENFA_LookaheadStart(AssertionType.Positive, _parentStart);
                                        _parentEnd = new ENFA_LookaheadEnd(_parentStart as ENFA_LookaheadStart, _parentEnd);
                                        break;
                                    case Constants.ExclamationMark:
                                        /* Consume ExclamationMark */
                                        ConsumeNextChar(reader);
                                        /* negative lookahead */
                                        _parentStart = new ENFA_LookaheadStart(AssertionType.Negative, _parentStart);
                                        _parentEnd = new ENFA_LookaheadEnd(_parentStart as ENFA_LookaheadStart, _parentEnd);
                                        break;
                                    case Constants.LessThanSign:
                                        /* Consume Less Than Sign */
                                        ConsumeNextChar(reader);
                                        char? tempNextChar3 = PeekNextChar(reader);
                                        if (tempNextChar3.Value == Constants.EqualsSign)
                                        {
                                            /* Consume Equals Sign */
                                            ConsumeNextChar(reader);
                                            /* positive lookbehind */
                                            _parentStart = new ENFA_LookbehindStart(AssertionType.Positive, _parentStart);
                                            _parentEnd = new ENFA_LookbehindEnd(_parentStart as ENFA_LookbehindStart, _parentEnd);
                                        }
                                        else if (tempNextChar3.Value == Constants.ExclamationMark)
                                        {
                                            /* Consume Exclamation Mark */
                                            ConsumeNextChar(reader);
                                            /* negative lookbehind */
                                            _parentStart = new ENFA_LookbehindStart(AssertionType.Negative, _parentStart);
                                            _parentEnd = new ENFA_LookbehindEnd(_parentStart as ENFA_LookbehindStart, _parentEnd);
                                        }
                                        else
                                        {
                                            /* named group */
                                            recording = true;
                                            groupName = GetGroupName(reader);
                                            _parentStart = new ENFA_GroupStart(recording, groupName, _parentStart);
                                            _parentEnd = new ENFA_GroupEnd(_parentStart as ENFA_GroupStart, _parentEnd);
                                        }
                                        break;
                                    default:
                                        ThrowBuildException(ErrorText.GroupingExpectedSpecifierAfterQuestionMark);
                                        break;
                                }
                            }
                            else
                            {
                                _parentStart = new ENFA_GroupStart(recording, groupName, _parentStart);
                                _parentEnd = new ENFA_GroupEnd(_parentStart as ENFA_GroupStart, _parentEnd);
                            }
                            if (_parentEnd is ENFA_LookbehindEnd)
                            {
                                nextState = _parentEnd;
                            }
                            else
                            {
                                nextState = _parentStart;
                            }
                            activeTransition = new ENFA_Regex_Transition(TransitionType.GroupingStart, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case Constants.GroupingEnd:
                            if(!(_parentStart is ENFA_GroupingStart))
                            {
                                ThrowBuildException(ErrorText.GroupingEndWithoutGroupingStart);
                            }
                            if (_parentEnd is ENFA_LookbehindEnd)
                            {
                                nextState = _parentStart;
                            }
                            else
                            {
                                nextState = _parentEnd;
                            }
                            activeTransition = new ENFA_Regex_Transition(TransitionType.GroupingEnd, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            _parentStart = _parentStart.Parent;
                            _parentEnd = _parentEnd.Parent;
                            break;
                        case Constants.VerticalLine:
                            if (_parentEnd is ENFA_LookbehindEnd)
                            {
                                nextState = _parentEnd;
                            }
                            else
                            {
                                nextState = _parentStart;
                            }
                            activeTransition = new ENFA_Regex_Transition(TransitionType.GroupingEnd, _parentEnd);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case Constants.PlusSign:
                            if (lastState is ENFA_PatternStart)
                            {
                                ThrowBuildException(ErrorText.PlusSignAsFirstCharInPattern);
                            }
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
                            if (lastState is ENFA_PatternStart)
                            {
                                ThrowBuildException(ErrorText.AsterixAsFirstCharInPattern);
                            }

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
                            if (lastState is ENFA_PatternStart)
                            {
                                ThrowBuildException(ErrorText.QuestionMarkAsFirstCharInPattern);
                            }
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
                            nextState = new ENFA_State(TransitionType.NewLine.ToString(), StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.NewLine, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'w':
                            nextState = new ENFA_State(TransitionType.Word.ToString(), StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Word, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'W':
                            nextState = new ENFA_State(TransitionType.NegateWord.ToString(), StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.NegateWord, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'd':
                            nextState = new ENFA_State(TransitionType.Digit.ToString(), StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Digit, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'D':
                            nextState = new ENFA_State(TransitionType.NegateDigit.ToString(), StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.NegateDigit, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 's':
                            nextState = new ENFA_State(TransitionType.Whitespace.ToString(), StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Whitespace, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'S':
                            nextState = new ENFA_State(TransitionType.NegateWhitespace.ToString(), StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.NegateWhitespace, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'l':
                            nextState = new ENFA_State(TransitionType.Letter.ToString(), StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.Letter, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'L':
                            nextState = new ENFA_State(TransitionType.NegateLetter.ToString(), StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.NegateLetter, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'b':
                            nextState = new ENFA_State(TransitionType.WordBoundary.ToString(), StateType.Transition);
                            activeTransition = new ENFA_Regex_Transition(TransitionType.WordBoundary, nextState);
                            lastState.AddTransition(activeTransition);
                            lastState = nextState;
                            break;
                        case 'B':
                            nextState = new ENFA_State(TransitionType.NegateWordBoundary.ToString(), StateType.Transition);
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
                            /* Named back reference like \k<Bartho> */
                            string groupName = null;
                            if (tempNextChar.HasValue && tempNextChar.Value == Constants.LessThanSign)
                            {
                                /* Consume LessThanSign */
                                ConsumeNextChar(reader);
                                groupName = GetGroupName(reader);
                            }
                            else
                            {
                                ThrowBuildException(ErrorText.NamedBackreferenceMissingStartGroupName);
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
                            ThrowBuildException(ErrorText.CharacterEscapedWithoutBeingExpectedTo);
                            break;
                    }
                    escaped = false;
                }
                if (!exit)
                {
                    nextChar = NextCharInStream(reader);
                }
            }
            return success;
        }

        private void ThrowBuildException(string message)
        {
            throw new ENFA_RegexBuild_Exception(_currectTerminalName, _matchedCharInRegexBuild.ToArray().ToString(), message);
        }

        private void CheckQuantifiers(StreamReader reader, out int minRepetitions, out int maxRepetitions, out MatchingType matchingType)
        {
            char matchedChar;
            minRepetitions = 0;
            maxRepetitions = -1;
            /* Run digits until comma or curly barce end */
            string firstDigit = GetStringUntilChar(reader, new char[] { Constants.Comma, Constants.RightCurlyBracket }, out matchedChar);
            string secondDigit = null;
            if (matchedChar == Constants.RightCurlyBracket && firstDigit == String.Empty)
            {
                ThrowBuildException(ErrorText.EmptyCurlyBraces);
            }
            if (firstDigit != String.Empty && !int.TryParse(firstDigit, out minRepetitions))
            {
                ThrowBuildException(ErrorText.CouldNotParseMinRepetitions);
            }
            else if (matchedChar == Constants.RightCurlyBracket)
            {
                maxRepetitions = minRepetitions;
            }
            if (matchedChar == Constants.Comma)
            {
                /* if comma is found then check digits until curly barce end */
                secondDigit = GetStringUntilChar(reader, new char[] { Constants.RightCurlyBracket }, out matchedChar);
                if (secondDigit == String.Empty)
                {
                    maxRepetitions = -1;
                }
                else if (!int.TryParse(secondDigit, out maxRepetitions))
                {
                    ThrowBuildException(ErrorText.CouldNotParseMaxRepetitions);
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
            char matchedChar;
            return GetStringUntilChar(reader, new char[] { Constants.GreaterThanSign }, out matchedChar);
        }

        private string GetStringUntilChar(StreamReader reader, char[] matchingChars, out char matchedChar)
        {
            StringBuilder matchedString = new StringBuilder();
            bool exit = false;
            char? tempMatchedChar = null;
            while (!exit)
            {
                char? nextChar = NextCharInStream(reader);
                if (!nextChar.HasValue)
                {
                    ThrowBuildException(ErrorText.EndOfStreamBeforeCharFound);
                }
                else
                {
                    foreach (char matchChar in matchingChars)
                    {
                        if (nextChar.Value == matchChar)
                        {
                            tempMatchedChar = nextChar.Value;
                            exit = true;
                            break;
                        }
                    }
                    if(!exit)
                    {
                        matchedString.Append(nextChar.Value);
                    }
                }
            }
            if(tempMatchedChar.HasValue)
            {
                matchedChar = tempMatchedChar.Value;
            }
            else
            {
                ThrowBuildException(ErrorText.EndOfStreamBeforeCharFound);
                matchedChar = Constants.NullChar;// Never hit this
            }
            return matchedString.ToString();
        }

        private void AddCharacterGroup(ENFA_Regex_Transition activeTransition, StreamReader reader)
        {
            bool exit = false;
            if(PeekNextChar(reader) == Constants.RightSquareBracket)
            {
                ThrowBuildException(ErrorText.CharacterClassEmpty);
            }
            while (!exit)
            {
                char? nextChar = NextCharInStream(reader);
                char? tempNextChar = PeekNextChar(reader);
                if (!nextChar.HasValue)
                {
                    exit = true;
                }
                else
                {
                    if(nextChar.Value == Constants.RightSquareBracket)
                    {
                        exit = true;
                    }
                    else if(nextChar.Value == Constants.Backslash)
                    {
                        if (tempNextChar.Value == 'n')
                        {
                            ConsumeNextChar(reader);// Consume New Line
                            activeTransition.AddLiteral(Constants.NewLine);
                        }
                        else if (tempNextChar.Value == 'v')
                        {
                            ConsumeNextChar(reader);// Consume Vertical Tab
                            activeTransition.AddLiteral(Constants.VerticalTab);
                        }
                        else if (tempNextChar.Value == 't')
                        {
                            ConsumeNextChar(reader);// Consume Horizontal Tab
                            activeTransition.AddLiteral(Constants.HorizontalTab);
                        }
                        else if (tempNextChar.Value == Constants.Backslash)
                        {
                            ConsumeNextChar(reader);// Consume Backslash
                            activeTransition.AddLiteral(Constants.Backslash);
                        }
                        else if (tempNextChar.Value == 'f')
                        {
                            ConsumeNextChar(reader);// Consume Form Feed
                            activeTransition.AddLiteral(Constants.FormFeed);
                        }
                        else if (tempNextChar.Value == 'r')
                        {
                            ConsumeNextChar(reader);// Consume Carriage Return
                            activeTransition.AddLiteral(Constants.CarriageReturn);
                        }
                        else if (tempNextChar.Value == '0')
                        {
                            ConsumeNextChar(reader);// Consume Null Char
                            activeTransition.AddLiteral(Constants.NullChar);
                        }
                        else if (tempNextChar.Value == 'a')
                        {
                            ConsumeNextChar(reader);// Consume Alert
                            activeTransition.AddLiteral(Constants.Alert);
                        }
                        else if (tempNextChar.Value == 'e')
                        {
                            ConsumeNextChar(reader);// Consume Escape
                            activeTransition.AddLiteral(Constants.Escape);
                        }
                        else if (tempNextChar.Value == 'y')
                        {
                            ConsumeNextChar(reader);// Consume Backspace
                            activeTransition.AddLiteral(Constants.Backspace);
                        }
                        else
                        {
                            ThrowBuildException(ErrorText.CharacterClassEscapedWithoutBeingExpectedTo);
                        }
                    }
                    else
                    {
                        if(tempNextChar.HasValue && tempNextChar.Value == Constants.HyphenMinusSign)
                        {
                            /* Char range */
                            ConsumeNextChar(reader);// Consume Hyphen Minus Sign
                            char? endRange = NextCharInStream(reader);
                            if(!endRange.HasValue)
                            {
                                ThrowBuildException(ErrorText.CharacterRangeHasNoEndValue);
                            }
                            int lowCounter = (int)nextChar.Value;
                            int highCounter = (int) endRange.Value;
                            if(lowCounter > highCounter)
                            {
                                int temp = lowCounter;
                                lowCounter = highCounter;
                                highCounter = temp;
                            }
                            for(int counter = lowCounter; counter <= highCounter; counter += 1)
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
                char next = (char)reader.Read();
                _matchedCharInRegexBuild.Add(next);
                return next;
            }
        }
    }
}
