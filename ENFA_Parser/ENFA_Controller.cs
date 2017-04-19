using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public class ENFA_Controller
    {
        private ParserType _parserType;
        private ENFA_Tokenizer _grammarTokenizer;

        public ENFA_Controller(ParserType parserType)
        {
            _parserType = parserType;
            switch(_parserType)
            {
                case ParserType.Regex:
                    _grammarTokenizer = new ENFA_Regex_Tokenizer(this);
                    break;
                case ParserType.Language:
                    _grammarTokenizer = new ENFA_Language_Tokenizer(this);
                    break;
            }
        }

        public ParserType ParserType
        {
            get
            {
                return _parserType;
            }
        }

        public ENFA_Tokenizer GrammarTokenizer
        {
            get
            {
                return _grammarTokenizer;
            }
            set
            {
                _grammarTokenizer = value;
            }
        }

        public bool BuildENFA(Stream stream)
        {
            AddStream(stream);
            //foreach(var data in GrammarTokenizer.Tokenize(Reader) )
            //{

            //}
            return true;
        }

        private StreamReader _streamReader;

        public void AddStream(Stream stream)
        {
            _streamReader = new StreamReader(new BufferedStream(stream, Constants.BufferSize), Constants.StreamEncoding, Constants.DetectStreamEncodingFromByteOrderMarks);
        }

        public StreamReader Reader
        {
            get
            {
                return _streamReader;
            }
        }

        public IEnumerable<ENFA_Base> Match(Stream stream)
        {
            yield return new ENFA_ResetCount(StateType.Negating);
        }
    }
}
