using System.Drawing;

namespace TagsCloudVisualization;

public class CircularCloudLayouter(IPointGenerator pointGenerator) : ICloudLayouter
{
    private readonly List<Rectangle> rectangles = [];
    
    public Rectangle PutNextRectangle(Size rectangleSize, Point center)
    {
        while (true)
        {
            var point = pointGenerator.GenerateNextPoint(center);
            var rect = CreateRectangleWithCenter(point, rectangleSize);

            if (rectangles.Any(r => r.IntersectsWith(rect)))
                continue;
                
            rectangles.Add(rect);
            return rect;
        }
    }

    private Rectangle CreateRectangleWithCenter(Point center, Size size)
    {
        var x = center.X - size.Width / 2;
        var y = center.Y - size.Height / 2;
        return new Rectangle(new Point(x, y), size);
    }
}