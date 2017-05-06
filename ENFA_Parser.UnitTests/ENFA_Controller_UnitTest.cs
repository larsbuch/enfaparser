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
        public void CreateGrammarInstanceWithoutError()
        {
            Exception expected = null;
            Exception actual = null;
            ENFA_Controller controller;
            try
            {
                controller = new ENFA_Controller(ParserType.Grammar);
            }
            catch (Exception ex)
            {
                actual = ex;
            }
            Assert.Equal(expected, actual);
        }

        [Theory, ENFAParserTestConvensions]
        public void SetTokenizer()
        {
            Exception expected = null;
            Exception actual = null;
            ENFA_Controller controller;
            try
            {
                controller = new ENFA_Controller(ParserType.Regex);
                controller.Tokenizer = new ENFA_Regex_Tokenizer(controller);
            }
            catch (Exception ex)
            {
                actual = ex;
            }
            Assert.Equal(expected, actual);
        }
    }
}
