using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENFA_Parser;
using System.IO;

namespace ENFA_Console
{
    public class Tests
    {
        public void SimpleConcatination()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"ab" + Constants.ExitContext;
            if (regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())))
            {
                Console.WriteLine(regex.PrintHierarchy);
                Assert.True(regex.Parser.Parse(new StreamReader("ab".ToStream())).ToList().Count > 0);
                Assert.False(regex.Parser.Parse(new StreamReader("a".ToStream())).ToList().Count > 0);
                Assert.False(regex.Parser.Parse(new StreamReader("b".ToStream())).ToList().Count > 0);
                Assert.False(regex.Parser.Parse(new StreamReader("ba".ToStream())).ToList().Count > 0);
            }
        }

        public void SimpleAlteration()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a|b" + Constants.ExitContext;
            if (regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())))
            {
                Console.WriteLine(regex.PrintHierarchy);
                Assert.True(regex.Parser.Parse(new StreamReader("a".ToStream())).ToList().Count > 0);
                Assert.True(regex.Parser.Parse(new StreamReader("b".ToStream())).ToList().Count > 0);
                Assert.False(regex.Parser.Parse(new StreamReader("ab".ToStream())).ToList().Count > 0);
                Assert.False(regex.Parser.Parse(new StreamReader("c".ToStream())).ToList().Count > 0);
            }
        }

        public void SingleLetter_Accept()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a" + Constants.ExitContext;
            regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream()));
            string regexString = @"a";
            Console.WriteLine(regex.PrintHierarchy);
            Assert.True(regex.Parser.Parse(new StreamReader(regexString.ToStream())).ToList().Count > 0);
        }


        private class Assert
        {
            internal static void False(bool input)
            {
                Console.WriteLine("False expected: " + (input ? "True" : "False"));
            }

            internal static void True(bool input)
            {
                Console.WriteLine("True expected: " + (input ? "True" : "False"));
            }
        }
    }
}
