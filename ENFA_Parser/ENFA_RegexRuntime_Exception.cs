using System;
using System.Runtime.Serialization;

namespace ENFA_Parser
{
    [Serializable]
    public class ENFA_RegexRuntime_Exception : ENFA_Exception
    {
        public ENFA_RegexRuntime_Exception(string message) : base(message)
        {
        }

        public ENFA_RegexRuntime_Exception(string terminalName, string matchedSofar, string message) : base(string.Format("Terminal {0} [{1}]: {2}", terminalName, matchedSofar, message))
        {
        }

        public ENFA_RegexRuntime_Exception(string terminalName, string matchedSofar, string message, Exception innerException) : base(string.Format("Terminal {0} [{1}]: {2}",terminalName, matchedSofar, message), innerException)
        {
        }

        protected ENFA_RegexRuntime_Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}