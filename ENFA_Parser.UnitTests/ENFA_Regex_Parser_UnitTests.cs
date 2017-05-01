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
            regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream()));
            string regexString = @"a";
            Assert.True(regex.Parser.ParseStream(new StreamReader(regexString.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SingleLetter_Reject()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a" + Constants.ExitContext;
            regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream()));
            string regexString = @"b";
            Assert.False(regex.Parser.ParseStream(new StreamReader(regexString.ToStream())));
        }
    }
}
