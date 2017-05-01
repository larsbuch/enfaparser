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
        private MatchingType _matchingType;
        private ENFA_PatternStart _patternStart;
        private bool _defaultGroupingRecording;
        private bool _inDebugMode;

        public ENFA_Controller(ParserType parserType)
        {
            _patternStart = new ENFA_PatternStart();
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
            _matchingType = MatchingType.LazyMatching;
            _inDebugMode = false;
        }

        public MatchingType DefaultMatchType
        {
            get
            {
                return _matchingType;
            }
            set
            {
                _matchingType = value;
            }
        }

        public ParserType ParserType
        {
            get
            {
                return _parserType;
            }
        }

        public ENFA_PatternStart PatternStart
        {
            get
            {
                return _patternStart;
            }
        }

        public bool DefaultGroupingRecording
        {
            get
            {
                return _defaultGroupingRecording;
            }
            set
            {
                _defaultGroupingRecording = value;
            }
        }

        public bool InDebugMode
        {
            get
            {
                return _inDebugMode;
            }
            set
            {
                _inDebugMode = value;
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
