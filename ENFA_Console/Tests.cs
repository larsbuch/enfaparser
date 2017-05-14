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
                Assert.True(regex.Parser.Stream(new StreamReader("ab".ToStream())));
                Assert.False(regex.Parser.Stream(new StreamReader("a".ToStream())));
                Assert.False(regex.Parser.Stream(new StreamReader("b".ToStream())));
                Assert.False(regex.Parser.Stream(new StreamReader("ba".ToStream())));
            }
        }

        public void SimpleAlteration()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a|b" + Constants.ExitContext;
            if (regex.Tokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())))
            {
                Console.WriteLine(regex.PrintHierarchy);
                Assert.True(regex.Parser.Stream(new StreamReader("a".ToStream())));
                Assert.True(regex.Parser.Stream(new StreamReader("b".ToStream())));
                Assert.False(regex.Parser.Stream(new StreamReader("ab".ToStream())));
                Assert.False(regex.Parser.Stream(new StreamReader("c".ToStream())));
            }
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
