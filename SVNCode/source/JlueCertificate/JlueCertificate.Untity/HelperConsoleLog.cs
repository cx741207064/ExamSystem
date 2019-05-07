using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JlueCertificate.Untity
{
    public class HelperConsoleLog
    {

        public static void printError(string error)
        {
            try
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.ResetColor();
            }
            catch (Exception)
            {
            }
        }
    }
}
