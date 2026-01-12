namespace TagsCloudVisualization;

public class WordFontSizeCalculator
{
    private const double minFontSize = 8.0;
    private readonly double maxFontSize = 72.0;
    
    public Dictionary<string, float> CalculateFontSizes(Dictionary<string, int> wordFrequencies)
    {
        var maxFrequency = wordFrequencies.Values.Max();
        var result = new Dictionary<string, float>();

        foreach (var (word, frequency) in wordFrequencies)
        {
            var normalizedFrequency = (double)frequency / maxFrequency;
            var fontSize = minFontSize + (normalizedFrequency * (maxFontSize - minFontSize));
            result[word] = (float)Math.Round(fontSize, 1);
        }

        return result;
    }
}