namespace TagsCloudVisualization;

public class DefaultAppPaths : IAppPaths
{
    public string DefaultBlacklistPath { get; }

    public DefaultAppPaths()
    {
        var fileName = "default_blacklist.txt";
        DefaultBlacklistPath = Path.Combine(AppContext.BaseDirectory, fileName);
    }
}

