using System.Drawing;

namespace TagsCloudVisualization;

public class SquarePointGenerator : IPointGenerator
{
    private int stepSize = 5;    
    private int stepsInCurrentLeg; 
    private int stepsInLegLimit = 1; 
    private int legProgress = 0;  
    private int legCount;    

    private int dx = 1;          
    private int dy;

    private int currentXOffset;
    private int currentYOffset;

    public Point GenerateNextPoint(Point center)
    {
        currentXOffset += dx * stepSize;
        currentYOffset += dy * stepSize;
        stepsInCurrentLeg++;

        if (stepsInCurrentLeg < stepsInLegLimit)
            return new Point(center.X + currentXOffset, center.Y + currentYOffset);
        stepsInCurrentLeg = 0;
        legCount++;
            
        var newDx = dy;
        var newDy = -dx;
        dx = newDx;
        dy = newDy;
            
        if (legCount % 2 == 0)
            stepsInLegLimit++;

        return new Point(center.X + currentXOffset, center.Y + currentYOffset);
    }
}