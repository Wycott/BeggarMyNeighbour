namespace BeggarMyNeighbourLibrary
{
    public class DealResult
    {
        private const int HalfDeckSize = 26;

        public int Cards { get; set; }
        public int Tricks { get; set; }
        public string PlayerOneDeal { get; set; } = string.Empty;
        public string PlayerTwoDeal { get; set; } = string.Empty;
        public string PlayerOneOutcome { get; set; } = string.Empty;
        public string PlayerTwoOutcome { get; set; } = string.Empty;

        public int PlayerOneSuits => HalfDeckSize - CountCharacterInstances(PlayerOneDeal, '-');

        public int PlayerTwoSuits => HalfDeckSize - CountCharacterInstances(PlayerTwoDeal, '-');

        private static int CountCharacterInstances(string input, char charToCount)
        {
            return input.Count(c => c == charToCount);
        }
    }
}
