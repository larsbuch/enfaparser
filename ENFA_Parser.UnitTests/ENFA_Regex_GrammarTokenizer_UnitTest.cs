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
            ENFA_Regex_Tokenizer regex = new ENFA_Regex_Tokenizer();
            string regexPattern = @"a";
            Assert.True(regex.Tokenize("State", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleAlteration()
        {
            ENFA_Regex_Tokenizer regex = new ENFA_Regex_Tokenizer();
            string regexPattern = @"a|b";
            Assert.True(regex.Tokenize("State", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleConcatination()
        {
            ENFA_Regex_Tokenizer regex = new ENFA_Regex_Tokenizer();
            string regexPattern = @"ab";
            Assert.True(regex.Tokenize("State", new StreamReader(regexPattern.ToStream())));
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
