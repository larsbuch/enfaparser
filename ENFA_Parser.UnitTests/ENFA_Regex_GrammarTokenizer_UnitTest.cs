using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ENFA_Parser.UnitTests
{
    public class ENFA_Regex_GrammarTokenizer_UnitTest
    {
        [Theory, ENFAParserTestConvensions]
        public void SingleLetter()
        {
            ENFA_Regex_Tokenizer regex = new ENFA_Regex_Tokenizer(new ENFA_Controller(ParserType.Regex));
            string regexPattern = @"a";
            Assert.False(regex.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleAlteration()
        {
            ENFA_Regex_Tokenizer regex = new ENFA_Regex_Tokenizer(new ENFA_Controller(ParserType.Regex));
            string regexPattern = @"a|b";
            Assert.False(regex.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleConcatination()
        {
            ENFA_Regex_Tokenizer regex = new ENFA_Regex_Tokenizer(new ENFA_Controller(ParserType.Regex));
            string regexPattern = @"ab";
            Assert.False(regex.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        //[Theory, ENFAParserTestConvensions]
        //public void SimpleNegation()
        //{
        //    ENFA_Regex_GrammarTokenizer regex = new ENFA_Regex_GrammarTokenizer();
        //    string regexPattern = @"[^a]";
        //    ENFA_StartingState startingState = new ENFA_StartingState(StateType.Transition);
        //    Assert.True(regex.Tokenize(startingState, "State", new StreamReader(regexPattern.ToStream())));
        //}

    }
}
