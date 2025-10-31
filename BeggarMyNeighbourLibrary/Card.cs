namespace BeggarMyNeighbourLibrary;

public record Card(char SimpleRank)
{
    public char SimpleRank { get; set; } = SimpleRank;
    public string SortOrder { get; set; } = Guid.NewGuid().ToString();
}