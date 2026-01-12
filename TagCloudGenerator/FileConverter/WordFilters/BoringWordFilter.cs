namespace TagsCloudVisualization;

public class BoringWordFilter : IWordFilter
{
    private const double boringWordFrequencyThreshold = 0.05;
    private const int minContentWordLength = 4;
    private const int minWordsCount = 2;

    public Dictionary<string, int> Filter(Dictionary<string, int> rawCounts, int totalWords, HashSet<string>? blackSet)
    {
        var result = new Dictionary<string, int>();
        blackSet ??= [];
        foreach (var (word, count) in rawCounts)
        {
            if (IsBoring(word, count, totalWords) || blackSet.Contains(word))
                continue;

            result[word] = count;
        }
        return result;
    }

    private bool IsBoring(string word, int count, int totalWords)
    {
        if (string.IsNullOrEmpty(word))
            return true;

        var isShort = word.Length < minContentWordLength;

        var frequency = (double)count / totalWords;

        if (isShort && frequency >= boringWordFrequencyThreshold)
            return true;

        return count < minWordsCount;
    }
}