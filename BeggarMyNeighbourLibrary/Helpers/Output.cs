using static System.Console;

namespace BeggarMyNeighbourLibrary.Helpers
{
    public static class Output
    {
        public static void WriteResults(string filenameStub, params string[] outputLines)
        {
            var fullFileName = filenameStub + "-BmnResults.txt";

            foreach (var line in outputLines)
            {
                WriteLine(line);
                File.AppendAllText(fullFileName, line + Environment.NewLine);
            }
        }
    }
}
