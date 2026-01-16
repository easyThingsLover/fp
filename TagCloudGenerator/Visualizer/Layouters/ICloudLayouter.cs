using System.Drawing;

namespace TagsCloudVisualization;

public interface ICloudLayouter
{
    ResultOf.Result<Rectangle> PutNextRectangle(Size rectangleSize, Point center, Size imageSize);
}