﻿using System;
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
            ENFA_GroupingStart _parentStart = new ENFA_PatternStart(ternimalName);
            ENFA_GroupingEnd _parentEnd = new ENFA_PatternEnd(_parentStart as ENFA_PatternStart);
            ENFA_Base lastState = _parentStart;
            ENFA_Base nextState;
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
                            exit = true;
                            break;
                        case Constants.StartOfLine:
                            nextState = new ENFA_State("Start of Line", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.StartOfLine, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case Constants.AllButNewLine:
                            nextState = new ENFA_State("Negate New Line", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.NegateNewLine, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case Constants.EndOfLine:
                            nextState = new ENFA_State("End of Line", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.EndOfLine, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case Constants.LeftCurlyBracket:
                            // TODO
                            throw new NotImplementedException();
                            break;
                        case Constants.LeftSquareBracket:
                            if ((char)reader.Peek() == Constants.CircumflexAccent)
                            {
                                nextState = new ENFA_State("Negate Character Group", StateType.Transition);
                                /* Remove CircumfelxAccent */
                                ConsumeNextChar(reader);
                                nextTransition = new ENFA_Regex_Transition(TransitionType.NegateLiteral, nextState);
                            }
                            else
                            {
                                nextState = new ENFA_State("Character Group", StateType.Transition);
                                nextTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            }
                            AddCharacterGroup(nextTransition, reader);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case Constants.LeftParanthesis:
                            bool recording = true;
                            string groupName = null;
                            if ((char)reader.Peek() == Constants.QuestionMark)
                            {
                                /* Consume QuetionMark */
                                ConsumeNextChar(reader);
                                // check for group name
                                if ((char)reader.Peek() == Constants.LessThanSign)
                                {
                                    groupName = GetGroupName(reader);
                                }
                                // check for non-recording group
                                else if ((char)reader.Peek() == Constants.Colon)
                                {
                                    recording = false;
                                }
                            }
                            _parentStart = new ENFA_GroupStart(_parentStart, recording, groupName);
                            _parentEnd = new ENFA_GroupEnd(_parentStart as ENFA_GroupStart, _parentEnd);
                            nextState = new ENFA_State("Group Start", StateType.NotApplicable);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.GroupingStart, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case Constants.RightParanthesis:
                            // TODO
                            throw new NotImplementedException();
                            break;
                        case Constants.VerticalLine:
                            // TODO
                            throw new NotImplementedException();
                            break;
                        case Constants.PlusSign:
                            // TODO
                            throw new NotImplementedException();
                            break;
                        case Constants.HyphenMinusSign:
                            // TODO
                            throw new NotImplementedException();
                            break;
                        case Constants.Asterisk:
                            // TODO
                            throw new NotImplementedException();
                            break;
                        case Constants.QuestionMark:
                            // TODO
                            throw new NotImplementedException();
                            break;
                        default:
                            nextState = new ENFA_State(nextChar.Value, StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            nextTransition.AddLiteral(nextChar.Value);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                    }
                }
                else
                {
                    /* Escaped characters */
                    switch (nextChar)
                    {
                        case '0':
                            nextState = new ENFA_State("Null Char", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            nextTransition.AddLiteral(Constants.NullChar);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'a':
                            nextState = new ENFA_State("Alert", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            nextTransition.AddLiteral(Constants.Alert);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'e':
                            nextState = new ENFA_State("Escape", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            nextTransition.AddLiteral(Constants.Escape);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'y':
                            nextState = new ENFA_State("Backspace", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            nextTransition.AddLiteral(Constants.Backspace);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'f':
                            nextState = new ENFA_State("Form Feed", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            nextTransition.AddLiteral(Constants.FormFeed);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'r':
                            nextState = new ENFA_State("Carriage Return", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            nextTransition.AddLiteral(Constants.CarriageReturn);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 't':
                            nextState = new ENFA_State("Horizontal Tab", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            nextTransition.AddLiteral(Constants.HorizontalTab);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'v':
                            nextState = new ENFA_State("Vertical Tab", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            nextTransition.AddLiteral(Constants.VerticalTab);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'n':
                            nextState = new ENFA_State("New Line", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.NewLine, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'w':
                            nextState = new ENFA_State("Word", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Word, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'W':
                            nextState = new ENFA_State("Negate Word", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.NegateWord, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'd':
                            nextState = new ENFA_State("Digit", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Digit, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'D':
                            nextState = new ENFA_State("Negate Digit", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.NegateDigit, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 's':
                            nextState = new ENFA_State("Whitespace", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Whitespace, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'S':
                            nextState = new ENFA_State("Negate Whitespace", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.NegateWhitespace, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'l':
                            nextState = new ENFA_State("Letter", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Letter, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'L':
                            nextState = new ENFA_State("Negate Letter", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.NegateLetter, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'b':
                            nextState = new ENFA_State("Word Boundary", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.WordBoundary, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'B':
                            nextState = new ENFA_State("Negate Word Boundary", StateType.Transition);
                            nextTransition = new ENFA_Regex_Transition(TransitionType.NegateWordBoundary, nextState);
                            lastState.AddTransition(nextTransition);
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
                            nextTransition = new ENFA_Regex_Transition(TransitionType.BackReference, nextState);
                            lastState.AddTransition(nextTransition);
                            lastState = nextState;
                            break;
                        case 'k':
                            /* Named back reference like k<Bartho> */
                            string groupName = null;
                            if ((char)reader.Peek() == Constants.LessThanSign)
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
                            nextTransition = new ENFA_Regex_Transition(TransitionType.BackReference, nextState);
                            lastState.AddTransition(nextTransition);
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
                            nextTransition = new ENFA_Regex_Transition(TransitionType.Literal, nextState);
                            nextTransition.AddLiteral(nextChar.Value);
                            lastState.AddTransition(nextTransition);
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
                if(exit || !nextChar.HasValue)
                {

                }
            }
            return error;
        }

        private string GetGroupName(StreamReader reader)
        {
            throw new NotImplementedException();
        }

        private void AddCharacterGroup(ENFA_Regex_Transition nextTransition, StreamReader reader)
        {
            throw new NotImplementedException();
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