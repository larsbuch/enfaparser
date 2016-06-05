using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ENFA_Parser.UnitTests
{
    public class ENFA_RegexGrammarTokenizer_UnitTest
    {
        [Theory, ENFAParserTestConvensions]
        public void SingleLetter()
        {
            ENFA_RegexGrammarTokenizer regex = new ENFA_RegexGrammarTokenizer();
            string regexPattern = @"a";
            List<ENFA_Step> steps = regex.Tokenize(new StreamReader(regexPattern.ToStream())).ToList();

            Assert.Equal(2, steps.Count);
            Assert.Equal("ENFA_StartingState", steps[0].State.StateName);
            Assert.Equal(regexPattern, steps[1].State.StateName);
            Assert.Equal(StateType.Accepting, steps[1].State.StateType);
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleAlteration()
        {
            ENFA_RegexGrammarTokenizer regex = new ENFA_RegexGrammarTokenizer();
            string regexPattern = @"a|b";
            List<ENFA_Step> steps = regex.Tokenize(new StreamReader(regexPattern.ToStream())).ToList();

            Assert.Equal(4, steps.Count);
            Assert.Equal("ENFA_StartingState", steps[0].State.StateName);
            Assert.Equal("a", steps[1].State.StateName);
            Assert.Equal(StateType.Accepting, steps[1].State.StateType);
            Assert.Equal("ENFA_StartingState", steps[2].State.StateName);
            Assert.Equal("b", steps[3].State.StateName);
            Assert.Equal(StateType.Accepting, steps[3].State.StateType);
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleConcatination()
        {
            ENFA_RegexGrammarTokenizer regex = new ENFA_RegexGrammarTokenizer();
            string regexPattern = @"ab";
            List<ENFA_Step> steps = regex.Tokenize(new StreamReader(regexPattern.ToStream())).ToList();

            Assert.Equal(4, steps.Count);
            Assert.Equal("ENFA_StartingState", steps[0].State.StateName);
            Assert.Equal("a", steps[1].State.StateName);
            Assert.Equal(StateType.Transition, steps[1].State.StateType);
            Assert.Equal("b", steps[2].State.StateName);
            Assert.Equal(StateType.Accepting, steps[2].State.StateType);
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleNegation()
        {
            ENFA_RegexGrammarTokenizer regex = new ENFA_RegexGrammarTokenizer();
            string regexPattern = @"a/b";
            List<ENFA_Step> steps = regex.Tokenize(new StreamReader(regexPattern.ToStream())).ToList();

            Assert.Equal(4, steps.Count);
            Assert.Equal("ENFA_StartingState", steps[0].State.StateName);
            Assert.Equal("a", steps[1].State.StateName);
            Assert.Equal(StateType.Transition, steps[1].State.StateType);
            Assert.Equal("b", steps[2].State.StateName);
            Assert.Equal(StateType.Negating, steps[2].State.StateType);
        }

    }
}
