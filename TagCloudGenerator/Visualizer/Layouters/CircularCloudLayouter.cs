using System.Drawing;
using ResultOf;

namespace TagsCloudVisualization;

public class CircularCloudLayouter(IPointGenerator pointGenerator) : ICloudLayouter
{
    private readonly List<Rectangle> rectangles = [];
    private const int MaxAttempts = 100000;

    public Result<Rectangle> PutNextRectangle(Size rectangleSize, Point center, Size imageSize)
    {
        var attempts = 0;
        var imageBounds = new Rectangle(0, 0, imageSize.Width, imageSize.Height);
        while (attempts++ < MaxAttempts)
        {
            var point = pointGenerator.GenerateNextPoint(center);
            var rect = CreateRectangleWithCenter(point, rectangleSize);

            if (!imageBounds.Contains(rect))
                continue;

            if (rectangles.Any(r => r.IntersectsWith(rect)))
                continue;

            rectangles.Add(rect);
            return Result.Ok(rect);
        }

        return Result.Fail<Rectangle>("Cloud doesn't fit into the image or couldn't find a non-intersecting placement after many attempts.");
    }

    private Rectangle CreateRectangleWithCenter(Point center, Size size)
    {
        var x = center.X - size.Width / 2;
        var y = center.Y - size.Height / 2;
        return new Rectangle(new Point(x, y), size);
    }
}