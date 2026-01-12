namespace TagsCloudVisualization;

public class FileConverter : IFileConverter
{
    private readonly FileWordCounter counter;
    private readonly IWordFilter filter;
    private readonly WordFontSizeCalculator wordFontSizeCalculator;
    private readonly IAppPaths appPaths;

    public FileConverter(FileWordCounter counter, IWordFilter filter, WordFontSizeCalculator wordFontSizeCalculator, IAppPaths appPaths)
    {
        this.counter = counter;
        this.filter = filter;
        this.wordFontSizeCalculator = wordFontSizeCalculator;
        this.appPaths = appPaths;
    }
    
    public Dictionary<string, float> Convert(string filePath, string blacklistFile = null)
    {
        var rawCounts = counter.CountWords(filePath, out var totalWords);

        var blackSet = new HashSet<string>();
        
        var defaultPath = appPaths.DefaultBlacklistPath;
        FillBlacklist(blackSet, defaultPath);
        FillBlacklist(blackSet, blacklistFile);
        var filteredWords = filter.Filter(rawCounts, totalWords, blackSet);
        return wordFontSizeCalculator.CalculateFontSizes(filteredWords);
    }

    private void FillBlacklist(HashSet<string> blackSet, string filePath)
    {
        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
        {
            foreach (var w in File.ReadAllLines(filePath))
                if (!string.IsNullOrWhiteSpace(w))
                    blackSet.Add(w.Trim().ToLower());
        }
    }
}
