using System;
using System.IO;
using System.Linq;
using Xunit;

namespace ENFA_Parser.UnitTests
{
    public class ENFA_Controller_UnitTest
    {
        [Theory, ENFAParserTestConvensions]
        public void CreateRegexInstanceWithoutError()
        {
            Exception expected = null;
            Exception actual = null;
            ENFA_Controller controller;
            try
            {
                controller = new ENFA_Controller(ParserType.Regex);
            }
            catch (Exception ex)
            {
                actual = ex;
            }
            Assert.Equal(expected, actual);
        }

        [Theory, ENFAParserTestConvensions]
        public void CreateLanguageInstanceWithoutError()
        {
            Exception expected = null;
            Exception actual = null;
            ENFA_Controller controller;
            try
            {
                controller = new ENFA_Controller(ParserType.Language);
            }
            catch (Exception ex)
            {
                actual = ex;
            }
            Assert.Equal(expected, actual);
        }

        [Theory, ENFAParserTestConvensions]
        public void SetGrammarTokenizer()
        {
            Exception expected = null;
            Exception actual = null;
            ENFA_Controller controller;
            try
            {
                controller = new ENFA_Controller(ParserType.Regex);
                controller.GrammarTokenizer = new ENFA_Regex_Tokenizer(controller);
            }
            catch (Exception ex)
            {
                actual = ex;
            }
            Assert.Equal(expected, actual);
        }

        //[Theory, ENFAParserTestConvensions]
        //public void BuildENFA_LiteralMatch()
        //{
        //    ENFA_Controller controller;
        //    controller = new ENFA_Controller(ParserType.Regex);
        //    controller.GrammarTokenizer = new ENFA_RegexGrammarTokenizer();

        //    string regex = "Literal";
        //    controller.BuildENFA(regex.ToStream());

        //    controller.Match(regex.ToStream());
        //    Assert.True(controller.AcceptingState);
        //}

        //[Theory, ENFAParserTestConvensions]
        //public void SingleLetter()
        //{
        //    ENFA_Regex_GrammarTokenizer regex = new ENFA_Regex_GrammarTokenizer();
        //    string regexPattern = @"a";
        //    ENFA_StartingState startingState = new ENFA_StartingState(StateType.Transition);
        //    if (regex.Tokenize(startingState, "State", new StreamReader(regexPattern.ToStream())))
        //    {
        //        Assert.True(startingState.CheckInput(new StreamReader("a".ToStream())));
        //        Assert.False(startingState.CheckInput(new StreamReader("aa".ToStream())));
        //        Assert.False(startingState.CheckInput(new StreamReader("b".ToStream())));
        //    }
        //}

        //[Theory, ENFAParserTestConvensions]
        //public void SimpleAlteration()
        //{
        //    ENFA_Regex_GrammarTokenizer regex = new ENFA_Regex_GrammarTokenizer();
        //    string regexPattern = @"a|b";
        //    ENFA_StartingState startingState = new ENFA_StartingState(StateType.Transition);
        //    if (regex.Tokenize(startingState, "State", new StreamReader(regexPattern.ToStream())))
        //    {
        //        Assert.True(startingState.CheckInput(new StreamReader("a".ToStream())));
        //        Assert.True(startingState.CheckInput(new StreamReader("b".ToStream())));
        //        Assert.False(startingState.CheckInput(new StreamReader("ab".ToStream())));
        //        Assert.False(startingState.CheckInput(new StreamReader("c".ToStream())));
        //    }
        //}

        //[Theory, ENFAParserTestConvensions]
        //public void SimpleConcatination()
        //{
        //    ENFA_Regex_GrammarTokenizer regex = new ENFA_Regex_GrammarTokenizer();
        //    string regexPattern = @"ab";
        //    ENFA_StartingState startingState = new ENFA_StartingState(StateType.Transition);
        //    if (regex.Tokenize(startingState, "State", new StreamReader(regexPattern.ToStream())))
        //    {
        //        Assert.True(startingState.CheckInput(new StreamReader("ab".ToStream())));
        //        Assert.False(startingState.CheckInput(new StreamReader("a".ToStream())));
        //        Assert.False(startingState.CheckInput(new StreamReader("b".ToStream())));
        //        Assert.False(startingState.CheckInput(new StreamReader("ba".ToStream())));
        //    }
        //}

        //[Theory, ENFAParserTestConvensions]
        //public void SimpleNegation()
        //{
        //    ENFA_Regex_GrammarTokenizer regex = new ENFA_Regex_GrammarTokenizer();
        //    string regexPattern = @"./b";
        //    ENFA_StartingState startingState = new ENFA_StartingState(StateType.Transition);
        //    if (regex.Tokenize(startingState, "State", new StreamReader(regexPattern.ToStream())))
        //    {
        //        Assert.True(startingState.CheckInput(new StreamReader("a".ToStream())));
        //        Assert.True(startingState.CheckInput(new StreamReader("c".ToStream())));
        //        Assert.False(startingState.CheckInput(new StreamReader("b".ToStream())));
        //    }
        //}

    }
}
