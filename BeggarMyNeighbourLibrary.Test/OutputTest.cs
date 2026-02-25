using AiAnnotations;
using BeggarMyNeighbourLibrary.Helpers;

namespace BeggarMyNeighbourLibrary.Test;

[AiGenerated]
public class OutputTest
{
    [Fact]
    public void WriteResults_CreatesFileWithContent()
    {
        var filenameStub = Guid.NewGuid().ToString();
        var fullFileName = filenameStub + "-BmnResults.txt";
        var lines = new[] { "Line1", "Line2" };

        try
        {
            Output.WriteResults(filenameStub, lines);

            Assert.True(File.Exists(fullFileName));
            var content = File.ReadAllText(fullFileName);
            Assert.Contains("Line1", content);
            Assert.Contains("Line2", content);
        }
        finally
        {
            if (File.Exists(fullFileName))
                File.Delete(fullFileName);
        }
    }

    [Fact]
    public void WriteResults_AppendsToExistingFile()
    {
        var filenameStub = Guid.NewGuid().ToString();
        var fullFileName = filenameStub + "-BmnResults.txt";

        try
        {
            Output.WriteResults(filenameStub, "First");
            Output.WriteResults(filenameStub, "Second");

            var content = File.ReadAllText(fullFileName);
            Assert.Contains("First", content);
            Assert.Contains("Second", content);
        }
        finally
        {
            if (File.Exists(fullFileName))
                File.Delete(fullFileName);
        }
    }
}
