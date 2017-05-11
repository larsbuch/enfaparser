using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ENFA_Parser.UnitTests
{
    public class ENFA_Regex_Parser_UnitTests
    {
        [Theory, ENFAParserTestConvensions]
        public void SingleLetter_Accept()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a" + Constants.ExitContext;
            regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream()));
            string regexString = @"a";
            Assert.True(regex.Parser.Stream(new StreamReader(regexString.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SingleLetter_Reject()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a" + Constants.ExitContext;
            regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream()));
            string regexString = @"b";
            Assert.False(regex.Parser.Stream(new StreamReader(regexString.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void BuildENFA_LiteralMatch()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexString = "Literal" + Constants.ExitContext;
            regex.Tokenizer.Tokenize(regexString, new StreamReader(regexString.ToStream()));
            Assert.False(regex.Parser.Stream(new StreamReader(regexString.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SingleLetter()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a" + Constants.ExitContext;
            if (regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())))
            {
                Assert.True(regex.Parser.Stream(new StreamReader("a".ToStream())));
                Assert.False(regex.Parser.Stream(new StreamReader("aa".ToStream())));
                Assert.False(regex.Parser.Stream(new StreamReader("b".ToStream())));
            }
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleAlteration()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a|b" + Constants.ExitContext;
            if (regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())))
            {
                Assert.True(regex.Parser.Stream(new StreamReader("a".ToStream())));
                Assert.True(regex.Parser.Stream(new StreamReader("b".ToStream())));
                Assert.False(regex.Parser.Stream(new StreamReader("ab".ToStream())));
                Assert.False(regex.Parser.Stream(new StreamReader("c".ToStream())));
            }
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleConcatination()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"ab" + Constants.ExitContext;
            if (regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())))
            {
                Assert.True(regex.Parser.Stream(new StreamReader("ab".ToStream())));
                Assert.False(regex.Parser.Stream(new StreamReader("a".ToStream())));
                Assert.False(regex.Parser.Stream(new StreamReader("b".ToStream())));
                Assert.False(regex.Parser.Stream(new StreamReader("ba".ToStream())));
            }
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleNegation()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"./b" + Constants.ExitContext;
            if (regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())))
            {
                Assert.True(regex.Parser.Stream(new StreamReader("a".ToStream())));
                Assert.True(regex.Parser.Stream(new StreamReader("c".ToStream())));
                Assert.False(regex.Parser.Stream(new StreamReader("b".ToStream())));
            }
        }
    }
}
