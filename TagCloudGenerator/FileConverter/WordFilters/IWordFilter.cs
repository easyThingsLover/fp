namespace TagsCloudVisualization;

public interface IWordFilter
{
    public Dictionary<string, int> Filter(Dictionary<string, int> rawCounts, int totalWords, HashSet<string> blackSet);
}