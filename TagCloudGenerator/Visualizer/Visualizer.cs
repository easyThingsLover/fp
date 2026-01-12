using System.Drawing;

namespace TagsCloudVisualization;

public class Visualizer(ICloudLayouter layouter) : IVisualizer
{
    public Bitmap DrawCloud(
        Dictionary<string, float> words,
        int imageWidth,
        int imageHeight,
        Color textColor,
        Color backgroundColor,
        string font)
    { 
        var bitmap = new Bitmap(imageWidth, imageHeight);
        var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(backgroundColor);
        var center = new Point(imageWidth / 2, imageHeight / 2);
        
        var textBrush = new SolidBrush(textColor);

        foreach (var (word, fontSize) in words)
        { 
            var currentFont = new Font(font, fontSize);
            var size = MeasureWord(word, graphics, currentFont);
            var rectangle = layouter.PutNextRectangle(size,  center);
            
            graphics.DrawString(word, currentFont, textBrush, rectangle.Location);
        }

        return bitmap;
    }

    private Size MeasureWord(string word, Graphics g, Font font)
    {
        var size = g.MeasureString(word, font);
        return new Size((int)size.Width, (int)size.Height);
    }
}