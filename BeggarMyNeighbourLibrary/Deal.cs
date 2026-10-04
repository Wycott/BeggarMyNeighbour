namespace BeggarMyNeighbourLibrary;

public class Deal
{
    public List<Card> PlayerOneCards { get; set; } = [];
    public List<Card> PlayerTwoCards { get; set; } = [];
    public string PlayerOneDeal { get; set; } = string.Empty;
    public string PlayerTwoDeal { get; set; } = string.Empty;

    public bool GoodCandidate
    {
        get
        {
            if (IsLast15AllDashes(PlayerOneDeal) || IsLast15AllDashes(PlayerTwoDeal))
            {
                return false;
            }

            if (HasTooManyFaceCardsInFirst15(PlayerOneDeal) || HasTooManyFaceCardsInFirst15(PlayerTwoDeal))
            {
                return false;
            }

            if (HasFaceCardCluster(PlayerOneDeal) || HasFaceCardCluster(PlayerTwoDeal))
            {
                return false;
            }

            return true;
        }
    }

    private static bool IsLast15AllDashes(string deal)
    {
        if (deal.Length < 15)
        {
            return false;
        }

        return deal[^15..].All(c => c == '-');
    }

    private static bool HasTooManyFaceCardsInFirst15(string deal)
    {
        var first15 = deal.Length < 15 ? deal : deal[..15];

        return "AJQK".Any(faceCard => first15.Count(c => c == faceCard) > 2);
    }

    private static bool HasFaceCardCluster(string deal)
    {
        if (deal.Length < 6)
        {
            return false;
        }

        for (var start = 0; start <= deal.Length - 6; start++)
        {
            var window = deal.AsSpan(start, 6);

            foreach (var faceCard in "AJQK")
            {
                var count = 0;

                foreach (var c in window)
                {
                    if (c == faceCard)
                    {
                        count++;
                    }
                }

                if (count > 3)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
