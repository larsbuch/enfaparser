using System.Text;

namespace ENFA_Parser
{
    public static class Constants
    {
        public const int BufferSize = 512;
        public const bool DetectStreamEncodingFromByteOrderMarks = true;
        public static Encoding StreamEncoding = Encoding.UTF8;
    }
}