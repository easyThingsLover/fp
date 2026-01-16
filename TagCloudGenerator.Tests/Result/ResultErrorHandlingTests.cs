using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudGenerator.Tests.Result;

[TestFixture]
public class ResultErrorHandlingTests
{
    private IFileConverter converter;
    private PointGeneratorFactory generatorFactory;
    private IVisualizerFactory visualizerFactory;
    private TagCloudRunner runner;
    private string inputPath;
    private string blacklistPath;
    private string outputPath;

    [SetUp]
    public void SetUp()
    {
        converter = A.Fake<IFileConverter>();
        generatorFactory = new PointGeneratorFactory();
        visualizerFactory = A.Fake<IVisualizerFactory>();
        runner = new TagCloudRunner(converter, generatorFactory, visualizerFactory);

        inputPath = Path.GetTempFileName();
        blacklistPath = Path.GetTempFileName();
        outputPath = Path.GetTempFileName();
    }
    
    [Test]
    public void Run_WhenInputFileMissing_ReturnsError()
    {
        var options = new TagCloudOptions
        {
            InputFile = "nonexistent_input.txt",
            BlacklistFile = blacklistPath,
            OutputFile = outputPath,
            ImageWidth = 200,
            ImageHeight = 200,
            PointGenerator = 1,
            FontName = "Arial"
        };

        var result = runner.Run(options);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("Input file");
        result.Error.Should().Contain(options.InputFile);
    }

    [Test]
    public void Run_WhenBlacklistFileMissing_ReturnsError()
    {
        var options = new TagCloudOptions
        {
            InputFile = inputPath,
            BlacklistFile = "nonexistent_blacklist.txt",
            OutputFile = outputPath,
            ImageWidth = 200,
            ImageHeight = 200,
            PointGenerator = 1,
            FontName = "Arial"
        };

        var result = runner.Run(options);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("Blacklist file");
        result.Error.Should().Contain(options.BlacklistFile);
    }

    [Test]
    public void Run_WithInvalidFont_ReturnsError()
    {
        A.CallTo(() => converter.Convert(A<string>._, A<string>._))
            .Returns(new Dictionary<string, float> { ["word"] = 24f });

        visualizerFactory = new VisualizerFactory();
        runner = new TagCloudRunner(converter, generatorFactory, visualizerFactory);

        var options = new TagCloudOptions
        {
            InputFile = inputPath,
            BlacklistFile = blacklistPath,
            OutputFile = outputPath,
            ImageWidth = 200,
            ImageHeight = 200,
            PointGenerator = 1,
            FontName = "SomeNonexistentFont"
        };

        var result = runner.Run(options);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("Font");
        result.Error.Should().Contain(options.FontName);
    }

    [Test]
    public void Run_WhenCloudDoesNotFit_ReturnsError()
    {
        A.CallTo(() => converter.Convert(A<string>._, A<string>._))
            .Returns(new Dictionary<string, float> { ["word"] = 200f, ["another"] = 180f });

        visualizerFactory = new VisualizerFactory();
        runner = new TagCloudRunner(converter, generatorFactory, visualizerFactory);

        var options = new TagCloudOptions
        {
            InputFile = inputPath,
            BlacklistFile = blacklistPath,
            OutputFile = outputPath,
            ImageWidth = 100,
            ImageHeight = 50,
            PointGenerator = 1,
            FontName = "Arial"
        };

        var result = runner.Run(options);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("Cloud doesn't fit");
    }
    
    [Test]
    public void Run_WhenSizeIsNegative_ReturnsError()
    {
        A.CallTo(() => converter.Convert(A<string>._, A<string>._))
            .Returns(new Dictionary<string, float> { ["word"] = 200f, ["another"] = 180f });

        visualizerFactory = new VisualizerFactory();
        runner = new TagCloudRunner(converter, generatorFactory, visualizerFactory);

        var options = new TagCloudOptions
        {
            InputFile = inputPath,
            BlacklistFile = blacklistPath,
            OutputFile = outputPath,
            ImageWidth = -100,
            ImageHeight = -50,
            PointGenerator = 1,
            FontName = "Arial"
        };

        var result = runner.Run(options);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("Size");
    }
}
