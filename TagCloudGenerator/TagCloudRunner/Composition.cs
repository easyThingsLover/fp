using Pure.DI;

namespace TagsCloudVisualization;

public partial class Composition
{
    private static readonly object _setup = DI.Setup("Composition")
        .Bind<IPointGeneratorFactory>().To<PointGeneratorFactory>()
        .Bind<IWordFilter>().To<BoringWordFilter>()
        .Bind<FileWordCounter>().To<FileWordCounter>()
        .Bind<WordFontSizeCalculator>().To<WordFontSizeCalculator>()
        .Bind<IAppPaths>().To<DefaultAppPaths>()
        .Bind<IFileConverter>().To<FileConverter>()
        .Bind<IVisualizerFactory>().To<VisualizerFactory>()
        .Bind<ITagCloudRunner>().To<TagCloudRunner>()
        .Root<ITagCloudRunner>("Root");
}