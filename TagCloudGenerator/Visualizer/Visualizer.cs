using System.Drawing;
using ResultOf;

namespace TagsCloudVisualization;

public class Visualizer(ICloudLayouter layouter) : IVisualizer
{
    public Result<Bitmap> DrawCloud(
        Dictionary<string, float> words,
        int imageWidth,
        int imageHeight,
        Color textColor,
        Color backgroundColor,
        string font)
    {
        var families = FontFamily.Families.Select(f => f.Name).ToArray();
        if (!families.Any(name => string.Equals(name, font, StringComparison.OrdinalIgnoreCase)))
            return Result.Fail<Bitmap>($"Font '{font}' not found on the system.");

        var bitmap = new Bitmap(imageWidth, imageHeight);
        var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(backgroundColor);
        var center = new Point(imageWidth / 2, imageHeight / 2);

        var textBrush = new SolidBrush(textColor);

        foreach (var (word, fontSize) in words)
        {
            var currentFont = new Font(font, fontSize);
            var size = MeasureWord(word, graphics, currentFont);
            var rectResult = layouter.PutNextRectangle(size, center, new Size(imageWidth, imageHeight));
            if (!rectResult.IsSuccess)
                return Result.Fail<Bitmap>($"Failed placing word '{word}' (size {size.Width}x{size.Height}): {rectResult.Error}");

            var rectangle = rectResult.GetValueOrThrow();
            graphics.DrawString(word, currentFont, textBrush, rectangle.Location);
        }

        return Result.Ok(bitmap);
    }

    private Size MeasureWord(string word, Graphics g, Font font)
    {
        var size = g.MeasureString(word, font);
        return new Size((int)size.Width, (int)size.Height);
    }
}