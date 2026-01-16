using System.Drawing;
using ResultOf;

namespace TagsCloudVisualization;

public interface IVisualizer
{
    Result<Bitmap> DrawCloud(Dictionary<string, float> words, int imageWidth, int imageHeight, Color textColor, Color backgroundColor, string font);
}
