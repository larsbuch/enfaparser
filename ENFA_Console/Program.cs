using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENFA_Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            Tests tests = new Tests();
            try
            {
                tests.SingleLetter_Accept();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            Console.Read();
        }
    }
}
