using System.Drawing;

namespace TagsCloudVisualization;

public class TagCloudOptions
{
    public string InputFile { get; init; }
    public string OutputFile { get; init; }
    public int ImageWidth { get; init; }
    public int ImageHeight { get; init; }
    public Color TextColor { get; init; }
    public Color BackgroundColor { get; init; }
    public string FontName { get; init; }
    public int PointGenerator { get; init; }
    public string BlacklistFile { get; init; }
}
