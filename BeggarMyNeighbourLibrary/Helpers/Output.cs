using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace BeggarMyNeighbourLibrary.Helpers
{
    public static class Output
    {
        public static void WriteResults(string filenameStub, string resultLine1, string resultLine2, string resultLine3,
            string resultLine4, string resultLine5, string resultLine6)
        {
            string fullfilename = filenameStub + "-BmnResults.txt";

            WriteLine(resultLine1);
            WriteLine(resultLine2);
            WriteLine(resultLine3);
            WriteLine(resultLine4);
            WriteLine(resultLine5);
            WriteLine(resultLine6);

            File.AppendAllText(fullfilename, resultLine1 + Environment.NewLine);
            File.AppendAllText(fullfilename, resultLine2 + Environment.NewLine);
            File.AppendAllText(fullfilename, resultLine3 + Environment.NewLine);
            File.AppendAllText(fullfilename, resultLine4 + Environment.NewLine);
            File.AppendAllText(fullfilename, resultLine5 + Environment.NewLine);
            File.AppendAllText(fullfilename, resultLine6 + Environment.NewLine);
        }
    }
}
