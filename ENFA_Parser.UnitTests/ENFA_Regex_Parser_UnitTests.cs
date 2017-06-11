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
            Assert.True(regex.Parser.Parse(new StreamReader(regexString.ToStream())).ToList().Count > 0);
        }

        [Theory, ENFAParserTestConvensions]
        public void SingleLetter_Reject()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a" + Constants.ExitContext;
            regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream()));
            string regexString = @"b";
            Assert.False(regex.Parser.Parse(new StreamReader(regexString.ToStream())).ToList().Count > 0);
        }

        [Theory, ENFAParserTestConvensions]
        public void BuildENFA_LiteralMatch()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexString = "Literal" + Constants.ExitContext;
            regex.Tokenizer.Tokenize(regexString, new StreamReader(regexString.ToStream()));
            Assert.False(regex.Parser.Parse(new StreamReader(regexString.ToStream())).ToList().Count > 0);
        }

        [Theory, ENFAParserTestConvensions]
        public void SingleLetter()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a" + Constants.ExitContext;
            if (regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())))
            {
                Assert.True(regex.Parser.Parse(new StreamReader("a".ToStream())).ToList().Count > 0);
                Assert.False(regex.Parser.Parse(new StreamReader("aa".ToStream())).ToList().Count > 0);
                Assert.False(regex.Parser.Parse(new StreamReader("b".ToStream())).ToList().Count > 0);
            }
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleAlteration()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a|b" + Constants.ExitContext;
            if (regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())))
            {
                Assert.True(regex.Parser.Parse(new StreamReader("a".ToStream())).ToList().Count > 0);
                Assert.True(regex.Parser.Parse(new StreamReader("b".ToStream())).ToList().Count > 0);
                Assert.False(regex.Parser.Parse(new StreamReader("ab".ToStream())).ToList().Count > 0);
                Assert.False(regex.Parser.Parse(new StreamReader("c".ToStream())).ToList().Count > 0);
            }
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleConcatination()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"ab" + Constants.ExitContext;
            if (regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())))
            {
                Assert.True(regex.Parser.Parse(new StreamReader("ab".ToStream())).ToList().Count > 0);
                Assert.False(regex.Parser.Parse(new StreamReader("a".ToStream())).ToList().Count > 0);
                Assert.False(regex.Parser.Parse(new StreamReader("b".ToStream())).ToList().Count > 0);
                Assert.False(regex.Parser.Parse(new StreamReader("ba".ToStream())).ToList().Count > 0);
            }
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleNegation()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"./b" + Constants.ExitContext;
            if (regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())))
            {
                Assert.True(regex.Parser.Parse(new StreamReader("a".ToStream())).ToList().Count > 0);
                Assert.True(regex.Parser.Parse(new StreamReader("c".ToStream())).ToList().Count > 0);
                Assert.False(regex.Parser.Parse(new StreamReader("b".ToStream())).ToList().Count > 0);
            }
        }
    }
}
