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
                controller.GrammarTokenizer = new ENFA_Regex_GrammarTokenizer();
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
    }
}
