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
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleAlteration()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a|b" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SimpleConcatination()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"ab" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void StartOfLine()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"^a" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void AllButNewLine()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"." + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void EndOfLine()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a$" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void Backspace()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"\\" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        #region Not Allowed Start Chars

        [Theory, ENFAParserTestConvensions]
        public void LeftCurlyBracketFirst_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"{0,1}" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void PlusSignFirst_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"+" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void QuestionMarkFirst_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"?" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void StarFirst_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"*" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void EOLFirst_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"$" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        #endregion

        #region Curly Bracket

        [Theory, ENFAParserTestConvensions]
        public void LeftCurlyBracketAlone_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"{" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void RightCurlyBracketAlone_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"}" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void LeftCurlyBracketEscaped_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"\{" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void RightCurlyBracketEscaped_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"\}" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void CurlyBracketAloneAndEmpty_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"{}" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void CurlyBracketEmpty_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a{}" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void CurlyBracketOneValue_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a{4}" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void CurlyBracketFirstValueEmpty_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a{,4}" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void CurlyBracketLastValueEmpty_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a{4,}" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void CurlyBracketFirstAndLastSpecified_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a{2,4}" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void CurlyBracketNonDigitOnFirstPosition_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a{a,5}" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void CurlyBracketNonDigitOnSecondPosition_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a{2,a}" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        #endregion

        #region Square Bracket

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketEmpty_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[]" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketConcatinated_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[ab]" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketEscaped_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[\n\v\t\\\f\0\a\e\y]" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketEscaped_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[\9\c]" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketRange_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[a-e]" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketRangeReversed_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[e-a]" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketConcatinatedInFrontRangeReversed_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[ae-a]" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketConcatinatedBehindRangeReversed_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[e-ab]" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketEmpty_Negate_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[^]" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketConcatinated_Negate_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[^ab]" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketEscaped_Negate_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[^\n\v\t\\\f\0\a\e\y]" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketEscaped_Negate_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[^\9\c]" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketRange_Negate_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[^a-e]" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketRangeReversed_Negate_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[^e-a]" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketConcatinatedInFrontRangeReversed_Negate_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[^ae-a]" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void SquareBracketConcatinatedBehindRangeReversed_Negate_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"[^e-ab]" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        #endregion

        #region Quantifiers

        [Theory, ENFAParserTestConvensions]
        public void Quantifiers_Star_Default_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"c*" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void Quantifiers_Star_Greedy_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"c*>" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void Quantifiers_Star_Lazy_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"c*?" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void Quantifiers_PlusSign_Default_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"c+" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void Quantifiers_PlusSign_Lazy_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"c+?" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void Quantifiers_PlusSign_Greedy_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"c+>" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void Quantifiers_QuestionMark_Default_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"c?" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void Quantifiers_QuestionMark_Lazy_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"c??" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void Quantifiers_QuestionMark_Greedy_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"c?>" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void Quantifiers_Repetition_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"c{1,5}" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        #endregion

        #region Grouping

        [Theory, ENFAParserTestConvensions]
        public void Grouping_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"(a)" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void GroupingConcatinatedInside_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"(ab)" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void GroupingConcatinatedOutsideInFront_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a(b)" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void GroupingConcatinatedOutsideBehind_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"(b)a" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void GroupingNamed_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"(?<Hans>b)a" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void GroupingNamed_Unnamed_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"(?<>b)a\k<>" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void GroupingNamed_BackreferenceNameDoesNotExist_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a\k<Andreas>" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void GroupingNamed_BackreferenceNumberDoesNotExist_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a\4" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void GroupingNonRecording_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"(?:b)a" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void GroupingPositiveLookahead_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a(?=b)" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void GroupingNegativeLookahead_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"a(?!b)" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void GroupingPositiveLookbehind_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"(?<=b)a" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void GroupingNegativeLookbehind_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"(?<!b)a" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void GroupingLookbehind_MissingMathingType_NotAllowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"(?<b)a" + Constants.ExitContext;
            Exception ex = Assert.Throws<ENFA_RegexBuild_Exception>(() => regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        #endregion

        #region Escaped Characters

        [Theory, ENFAParserTestConvensions]
        public void EscapedCharacters_Literal_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"\0\a\e\y\f\r\t\v\n" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void EscapedCharacters_CharacterGroups_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"\w\W\d\D\s\S\l\L" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void EscapedCharacters_WordBoundaries_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"\b\B" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void EscapedCharacters_NumericBackReferences_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"(?>1)(?>2)(?>3)(?>4)(?>5)(?>6)(?>7)(?>8)(?>9)\1\2\3\4\5\6\7\8\9" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void EscapedCharacters_NamedBackReferences_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"(?<Andreas>)\k<Andreas>" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        [Theory, ENFAParserTestConvensions]
        public void EscapedCharacters_SpecialCharacters_Allowed()
        {
            ENFA_Controller regex = new ENFA_Controller(ParserType.Regex);
            string regexPattern = @"\""\{\[\(\)\|\\\.\$\^\?\+\*" + Constants.ExitContext;
            Assert.True(regex.GrammarTokenizer.Tokenize("UnitTest", new StreamReader(regexPattern.ToStream())));
        }

        #endregion
    }
}
