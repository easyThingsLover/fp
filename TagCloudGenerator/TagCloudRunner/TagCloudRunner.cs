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

    public void Run(TagCloudOptions options)
    {
        var words = converter.Convert(options.InputFile, options.BlacklistFile);
        var generator = generatorFactory.Create(options.PointGenerator);
        var layouter = new CircularCloudLayouter(generator);
        var visualizer = visualizerFactory.Create(layouter);

        var image = visualizer.DrawCloud(words, options.ImageWidth, options.ImageHeight, options.TextColor, options.BackgroundColor, options.FontName);
        image.Save(options.OutputFile, System.Drawing.Imaging.ImageFormat.Png);
    }
}
