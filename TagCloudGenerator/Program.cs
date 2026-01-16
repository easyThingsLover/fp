using System.Drawing;
using CommandLine;
using TagsCloudVisualization;

public static class Program
{
    public static void Main(string[] args)
    {
        Parser.Default
            .ParseArguments<TagCloudCliOptions>(args)
            .MapResult(
                RunWithOptions,
                _ => 1);
    }

    private static int RunWithOptions(TagCloudCliOptions cli)
    {
        var textColor = Color.FromName(cli.TextColorName);
        var backgroundColor = Color.FromName(cli.BackgroundColorName);

            var options = new TagCloudOptions
        {
            InputFile = cli.InputFile,
            OutputFile = cli.OutputFile,
            ImageWidth = cli.ImageWidth,
            ImageHeight = cli.ImageHeight,
            FontName = cli.FontName,
            TextColor = textColor.IsKnownColor ? textColor : Color.Black,
            BackgroundColor = backgroundColor.IsKnownColor ? backgroundColor : Color.White,
            PointGenerator = AskForPointGenerator(),
            BlacklistFile = cli.BlacklistFile
        };

        var runner = new Composition().Root;
        var result = runner.Run(options);
        if (result.IsSuccess) return 0;
        Console.Error.WriteLine("Error: " + result.Error);
        return 1;

    }

    private static int AskForPointGenerator()
    {
        var options = new[] { "spiral", "square" };
        while (true)
        {
            Console.WriteLine("Select point generator:");
            for (var i = 0; i < options.Length; i++)
                Console.WriteLine($"  {i + 1}. {options[i]}");

            Console.Write("Enter number (default 1): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return 1;

            if (int.TryParse(input.Trim(), out var idx) && idx >= 1 && idx <= options.Length)
                return idx;

            Console.WriteLine("Invalid choice. Enter the digit corresponding to the generator.");
        }
    }
}