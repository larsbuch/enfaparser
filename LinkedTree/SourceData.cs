using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedTree
{
    public class SourceData
    {
        private string _sourceFile;
        private int _lineNumber;
        private int _charNumber;

        public SourceData(string sourceFile, int lineNumber, int charNumber)
        {
            _sourceFile = sourceFile;
            _lineNumber = lineNumber;
            _charNumber = charNumber;
        }

        public string SourceFile
        {
            get
            {
                return _sourceFile;
            }
        }

        public int LineNumber
        {
            get
            {
                return _lineNumber;
            }
        }

        public int CharNumber
        {
            get
            {
                return _charNumber;
            }
        }
    }
}
