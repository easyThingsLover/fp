using System.Drawing;

namespace TagsCloudVisualization;

public interface IVisualizer
{
    Bitmap DrawCloud(Dictionary<string, float> words, int imageWidth, int imageHeight, Color textColor, Color backgroundColor, string font);
}
