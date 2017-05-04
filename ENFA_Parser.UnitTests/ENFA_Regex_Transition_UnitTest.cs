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
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.Literal, new ENFA_State(controller, RegexTransitionType.Literal.ToString(), StateType.Accepting));
            transition.AddLiteral('a');
            transition.AddLiteral('b');
            Assert.True(transition.TransitionAllowed(null,'a', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void Literal_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.Literal, new ENFA_State(controller, RegexTransitionType.Literal.ToString(), StateType.Accepting));
            transition.AddLiteral('a');
            transition.AddLiteral('b');
            Assert.False(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateLiteral_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateLiteral, new ENFA_State(controller, RegexTransitionType.NegateLiteral.ToString(), StateType.Accepting));
            transition.AddLiteral('a');
            transition.AddLiteral('b');
            Assert.True(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateLiteral_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateLiteral, new ENFA_State(controller, RegexTransitionType.NegateLiteral.ToString(), StateType.Accepting));
            transition.AddLiteral('a');
            transition.AddLiteral('b');
            Assert.False(transition.TransitionAllowed(null, 'a', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void Letter_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.Letter, new ENFA_State(controller, RegexTransitionType.Letter.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void Letter_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.Letter, new ENFA_State(controller, RegexTransitionType.Letter.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, '9', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateLetter_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateLetter, new ENFA_State(controller, RegexTransitionType.NegateLetter.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, '9', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateLetter_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateLetter, new ENFA_State(controller, RegexTransitionType.NegateLetter.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void Digit_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.Digit, new ENFA_State(controller, RegexTransitionType.Digit.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, '9', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void Digit_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.Digit, new ENFA_State(controller, RegexTransitionType.Digit.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateDigit_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateDigit, new ENFA_State(controller, RegexTransitionType.NegateDigit.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateDigit_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateDigit, new ENFA_State(controller, RegexTransitionType.NegateDigit.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, '9', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void Whitespace_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.Whitespace, new ENFA_State(controller, RegexTransitionType.Whitespace.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, ' ', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void Whitespace_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.Whitespace, new ENFA_State(controller, RegexTransitionType.Whitespace.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, '9', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWhitespace_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateWhitespace, new ENFA_State(controller, RegexTransitionType.NegateWhitespace.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, 'c', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWhitespace_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateWhitespace, new ENFA_State(controller, RegexTransitionType.NegateWhitespace.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, ' ', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void Word_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.Word, new ENFA_State(controller, RegexTransitionType.Word.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, '_', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void Word_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.Word, new ENFA_State(controller, RegexTransitionType.Word.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, ' ', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWord_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateWord, new ENFA_State(controller, RegexTransitionType.NegateWord.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, '-', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWord_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateWord, new ENFA_State(controller, RegexTransitionType.NegateWord.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, 'a', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NewLine_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NewLine, new ENFA_State(controller, RegexTransitionType.NewLine.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, '\n', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NewLine_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NewLine, new ENFA_State(controller, RegexTransitionType.NewLine.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, 'a', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateNewLine_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateNewLine, new ENFA_State(controller, RegexTransitionType.NegateNewLine.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, ' ', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateNewLine_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateNewLine, new ENFA_State(controller, RegexTransitionType.NegateNewLine.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, '\n', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void WordBoundary_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.WordBoundary, new ENFA_State(controller, RegexTransitionType.WordBoundary.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, 'a', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void WordBoundary_Success2()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.WordBoundary, new ENFA_State(controller, RegexTransitionType.WordBoundary.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(' ', 'a', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void WordBoundary_Success3()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.WordBoundary, new ENFA_State(controller, RegexTransitionType.WordBoundary.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed('b', ' ', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void WordBoundary_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.WordBoundary, new ENFA_State(controller, RegexTransitionType.WordBoundary.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, '\n', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWordBoundary_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateWordBoundary, new ENFA_State(controller, RegexTransitionType.NegateWordBoundary.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed('a', 'b', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWordBoundary_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateWordBoundary, new ENFA_State(controller, RegexTransitionType.NegateWordBoundary.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(null, 'a', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWordBoundary_Fail2()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateWordBoundary, new ENFA_State(controller, RegexTransitionType.NegateWordBoundary.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed('b', ' ', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void NegateWordBoundary_Fail3()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.NegateWordBoundary, new ENFA_State(controller, RegexTransitionType.NegateWordBoundary.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed(' ', 'a', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void StartOfLine_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.StartOfLine, new ENFA_State(controller, RegexTransitionType.StartOfLine.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed(null, ' ', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void StartOfLine_Success2()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.StartOfLine, new ENFA_State(controller, RegexTransitionType.StartOfLine.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed('\n', ' ', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void StartOfLine_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.StartOfLine, new ENFA_State(controller, RegexTransitionType.StartOfLine.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed('a', 'c', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void EndOfLine_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.EndOfLine, new ENFA_State(controller, RegexTransitionType.EndOfLine.ToString(), StateType.Accepting));
            Assert.True(transition.TransitionAllowed('a', '\n', out transitionConsumesCharacter));
            Assert.False(transitionConsumesCharacter);
        }

        [Theory, ENFAParserTestConvensions]
        public void EndOfLine_Fail()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.EndOfLine, new ENFA_State(controller, RegexTransitionType.EndOfLine.ToString(), StateType.Accepting));
            Assert.False(transition.TransitionAllowed('\n', 'a', out transitionConsumesCharacter));
        }

        [Theory, ENFAParserTestConvensions]
        public void ExitContext_Success()
        {
            bool transitionConsumesCharacter;
            ENFA_Controller controller = new ENFA_Controller(ParserType.Regex);
            ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.ExitContext, new ENFA_ContextSwitch(controller, StateType.Accepting));
            Assert.True(transition.TransitionAllowed('"', '"', out transitionConsumesCharacter));
            Assert.True(transitionConsumesCharacter);
        }

        //// TODO identify if a fail is possible 
        //[Theory, ENFAParserTestConvensions]
        //public void ExitState_Fail()
        //{
        //    bool transitionConsumesCharacter;
        //    ENFA_Regex_Transition transition = (controller.Factory as ENFA_Regex_Factory).CreateRegexTransition(RegexTransitionType.ExitState, new ENFA_ContextSwitch(StateType.Accepting));
        //    Assert.False(transition.TransitionAllowed('"', 'a', out transitionConsumesCharacter));
        //}
    }
}
