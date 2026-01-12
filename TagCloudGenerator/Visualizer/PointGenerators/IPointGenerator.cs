using System.Drawing;

namespace TagsCloudVisualization;

public interface IPointGenerator
{
    public Point GenerateNextPoint(Point center);
}