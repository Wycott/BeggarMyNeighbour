using static System.Console;

namespace BeggarMyNeighbourLibrary.Helpers
{
    public static class Output
    {
        public static void WriteResults(string filenameStub, params string[] outputLines)
        {
            string fullfilename = filenameStub + "-BmnResults.txt";

            foreach (var line in outputLines)
            {
                WriteLine(line);
                File.AppendAllText(fullfilename, line + Environment.NewLine);
            }
        }
    }
}
