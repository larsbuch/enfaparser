using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ENFA_Parser.UnitTests
{
    public class ENFA_Regex_Transition_UnitTest
    {
        [Theory, ENFAParserTestConvensions]
        public void Literal_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.Literal, new ENFA_State(TransitionType.Literal.ToString(), StateType.Accepting));
            transition.AddLiteral('a');
            transition.AddLiteral('b');
            Assert.True(transition.TransitionAllowed(null,'a', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void Literal_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.Literal, new ENFA_State(TransitionType.Literal.ToString(), StateType.Accepting));
            transition.AddLiteral('a');
            transition.AddLiteral('b');
            Assert.False(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateLiteral_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateLiteral, new ENFA_State(TransitionType.NegateLiteral.ToString(), StateType.Accepting));
            transition.AddLiteral('a');
            transition.AddLiteral('b');
            Assert.True(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateLiteral_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateLiteral, new ENFA_State(TransitionType.NegateLiteral.ToString(), StateType.Accepting));
            transition.AddLiteral('a');
            transition.AddLiteral('b');
            Assert.False(transition.TransitionAllowed(null, 'a', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void Letter_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.Letter, new ENFA_State(TransitionType.Letter.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void Letter_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.Letter, new ENFA_State(TransitionType.Letter.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, '9', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateLetter_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateLetter, new ENFA_State(TransitionType.NegateLetter.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, '9', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateLetter_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateLetter, new ENFA_State(TransitionType.NegateLetter.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void Digit_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.Digit, new ENFA_State(TransitionType.Digit.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, '9', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void Digit_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.Digit, new ENFA_State(TransitionType.Digit.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateDigit_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateDigit, new ENFA_State(TransitionType.NegateDigit.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateDigit_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateDigit, new ENFA_State(TransitionType.NegateDigit.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, '9', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void Whitespace_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.Whitespace, new ENFA_State(TransitionType.Whitespace.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, ' ', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void Whitespace_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.Whitespace, new ENFA_State(TransitionType.Whitespace.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, '9', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWhitespace_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateWhitespace, new ENFA_State(TransitionType.NegateWhitespace.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWhitespace_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateWhitespace, new ENFA_State(TransitionType.NegateWhitespace.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, ' ', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void Word_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.Word, new ENFA_State(TransitionType.Word.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, '_', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void Word_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.Word, new ENFA_State(TransitionType.Word.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, ' ', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWord_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateWord, new ENFA_State(TransitionType.NegateWord.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, '-', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWord_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateWord, new ENFA_State(TransitionType.NegateWord.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, 'a', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NewLine_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NewLine, new ENFA_State(TransitionType.NewLine.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, '\n', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NewLine_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NewLine, new ENFA_State(TransitionType.NewLine.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, 'a', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateNewLine_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateNewLine, new ENFA_State(TransitionType.NegateNewLine.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, ' ', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateNewLine_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateNewLine, new ENFA_State(TransitionType.NegateNewLine.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, '\n', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void WordBoundary_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.WordBoundary, new ENFA_State(TransitionType.WordBoundary.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, 'a', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void WordBoundary_Success2()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.WordBoundary, new ENFA_State(TransitionType.WordBoundary.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(' ', 'a', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void WordBoundary_Success3()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.WordBoundary, new ENFA_State(TransitionType.WordBoundary.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed('b', ' ', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void WordBoundary_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.WordBoundary, new ENFA_State(TransitionType.WordBoundary.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, '\n', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWordBoundary_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateWordBoundary, new ENFA_State(TransitionType.NegateWordBoundary.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed('a', 'b', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWordBoundary_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateWordBoundary, new ENFA_State(TransitionType.NegateWordBoundary.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, 'a', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWordBoundary_Fail2()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateWordBoundary, new ENFA_State(TransitionType.NegateWordBoundary.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed('b', ' ', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWordBoundary_Fail3()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.NegateWordBoundary, new ENFA_State(TransitionType.NegateWordBoundary.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(' ', 'a', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void StartOfLine_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.StartOfLine, new ENFA_State(TransitionType.StartOfLine.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, ' ', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void StartOfLine_Success2()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.StartOfLine, new ENFA_State(TransitionType.StartOfLine.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed('\n', ' ', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void StartOfLine_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.StartOfLine, new ENFA_State(TransitionType.StartOfLine.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed('a', 'c', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void EndOfLine_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.EndOfLine, new ENFA_State(TransitionType.EndOfLine.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed('a', '\n', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void EndOfLine_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.EndOfLine, new ENFA_State(TransitionType.EndOfLine.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed('\n', 'a', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void ExitState_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.ExitState, new ENFA_ContextSwitch(StateType.Accepting));
            Assert.True(transition.TransitionAllowed('"', '"', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        //// TODO identify if a fail is possible 
        //[Theory, ENFAParserTestConvensions]
        //public void ExitState_Fail()
        //{
        //    bool transitionConsumesCharacter;
        //    ENFA_Regex_Transition transition = new ENFA_Regex_Transition(TransitionType.ExitState, new ENFA_ContextSwitch(StateType.Accepting));
        //    Assert.False(transition.TransitionAllowed('"', 'a', out transitionConsumesCharacter));
        //}
    }
}
