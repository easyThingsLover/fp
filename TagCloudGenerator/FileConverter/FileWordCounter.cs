using System.Text.RegularExpressions;

namespace TagsCloudVisualization;

public class FileWordCounter
{
    public Dictionary<string, int> CountWords(string filePath, out int totalWords)
    {
        var rawCounts = new Dictionary<string, int>();
        totalWords = 0;

        foreach (var line in File.ReadLines(filePath))
        {
            var word = NormalizeWord(line);
            if (string.IsNullOrEmpty(word))
                continue;

            totalWords++;

            if (rawCounts.TryGetValue(word, out var count))
                rawCounts[word] = count + 1;
            else
                rawCounts[word] = 1;
        }

        return rawCounts;
    }

    private string NormalizeWord(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return string.Empty;

        var trimmed = raw.Trim().ToLower();
        
        return Regex.IsMatch(trimmed, @"^[\p{L}'-]+$") ? trimmed : string.Empty;;
    }
}