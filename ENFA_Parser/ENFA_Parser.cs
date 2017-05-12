using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Parser
{
    public abstract class ENFA_Parser
    {
        private ENFA_Controller _controller;
        private StreamReader _streamReader;

        public ENFA_Parser(ENFA_Controller controller)
        {
            _controller = controller;
        }

        public ENFA_Controller Controller
        {
            get
            {
                return _controller;
            }
        }

        public StreamReader StreamReader
        {
            get
            {
                return _streamReader;
            }
            set
            {
                _streamReader = value;
            }
        }

        public bool Stream(StreamReader streamReader)
        {
            throw new NotImplementedException();
        }
    }
}
