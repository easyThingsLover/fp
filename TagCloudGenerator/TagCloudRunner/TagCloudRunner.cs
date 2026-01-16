using ResultOf;

namespace TagsCloudVisualization;

public class TagCloudRunner : ITagCloudRunner
{
    private readonly IFileConverter converter;
    private readonly IPointGeneratorFactory generatorFactory;
    private readonly IVisualizerFactory visualizerFactory;

    public TagCloudRunner(IFileConverter converter, IPointGeneratorFactory generatorFactory, IVisualizerFactory visualizerFactory)
    {
        this.converter = converter;
        this.generatorFactory = generatorFactory;
        this.visualizerFactory = visualizerFactory;
    }

    public Result<None> Run(TagCloudOptions options)
    {
        if (string.IsNullOrEmpty(options.InputFile) || !File.Exists(options.InputFile))
            return Result.Fail<None>($"Input file '{options.InputFile}' not found. Provide a correct path.");

        if (string.IsNullOrEmpty(options.BlacklistFile) || !File.Exists(options.BlacklistFile))
            return Result.Fail<None>($"Blacklist file '{options.BlacklistFile}' was not found. Provide a correct path.");

        var wordsResult = Result.Of(() => converter.Convert(options.InputFile, options.BlacklistFile), $"Failed reading or parsing input file '{options.InputFile}'");

        var result = wordsResult
            .Then(words => {
                var generator = generatorFactory.Create(options.PointGenerator);
                var layouter = new CircularCloudLayouter(generator);
                var visualizer = visualizerFactory.Create(layouter);
                return visualizer.DrawCloud(words, options.ImageWidth, options.ImageHeight, options.TextColor, options.BackgroundColor, options.FontName);
            })
            .Then(bitmap => Result.OfAction(() => bitmap.Save(options.OutputFile, System.Drawing.Imaging.ImageFormat.Png), $"Failed to save image to '{options.OutputFile}'"))
            .RefineError("Tag cloud creation failed");

        return result;
    }
}
