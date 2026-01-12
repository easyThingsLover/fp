namespace TagsCloudVisualization;

public interface IFileConverter
{
    Dictionary<string, float> Convert(string filePath, string blacklistFile);
}
