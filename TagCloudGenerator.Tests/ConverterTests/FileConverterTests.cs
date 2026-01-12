using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudGenerator.FileConverter.Tests;

[TestFixture]
public class FileConverterTests
{
    private string CreateTempFile(IEnumerable<string> lines)
    {
        var path = Path.GetTempFileName();
        File.WriteAllLines(path, lines);
        return path;
    }

    [Test]
    public void Convert_WhenOk_ReturnsCalculatedSizes()
    {
        var lines = new[] { "Hello", "hello", "World", "world", "content" };
        var path = CreateTempFile(lines);

        var counter = new FileWordCounter();
        var filter = new BoringWordFilter();
        var calculator = new WordFontSizeCalculator();

        var converter = new TagsCloudVisualization.FileConverter(counter, filter, calculator, new DefaultAppPaths());
        var result = converter.Convert(path);
            
        result.Should().ContainKey("hello");
        result["hello"].Should().Be(72f);
        result.Should().ContainKey("world");
        result["world"].Should().Be(72f);
        result.Should().NotContainKey("content");

        File.Delete(path);
    }

    [Test]
    public void Convert_WhenFilterRemovesAll_ThrowsFromCalculator()
    {
        var lines = new[] { "one", "two" };
        var path = CreateTempFile(lines);

        var counter = new FileWordCounter();
        var emptyFilter = new BoringWordFilter();
        var calculator = new WordFontSizeCalculator();

        var converter = new TagsCloudVisualization.FileConverter(counter, emptyFilter, calculator, new DefaultAppPaths());

        FluentActions.Invoking(() => converter.Convert(path))
            .Should().Throw<InvalidOperationException>();

        File.Delete(path);
    }

    [Test]
    public void Convert_WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        var counter = new FileWordCounter();
        var filter = new BoringWordFilter();
        var calculator = new WordFontSizeCalculator();

        var converter = new TagsCloudVisualization.FileConverter(counter, filter, calculator, new DefaultAppPaths());

        FluentActions.Invoking(() => converter.Convert("nonexistent_file_12345.tmp"))
            .Should().Throw<FileNotFoundException>();
    }
    
    [Test]
    public void Convert_NotContain_WordsFromBlackList()
    {
        var lines = new[] { "Hello", "hello", "World", "world", "content" };
        var blackList = new[] { "hello" };
        var blackPath = CreateTempFile(blackList);
        var path = CreateTempFile(lines);
        var counter = new FileWordCounter();
        var filter = new BoringWordFilter();
        var calculator = new WordFontSizeCalculator();

        var converter = new TagsCloudVisualization.FileConverter(counter, filter, calculator, new DefaultAppPaths());
        var result =  converter.Convert(path,blackPath);
        result.Should().NotContainKey("hello");
        result.Should().NotContainKey("Hello");
        File.Delete(path);
    }
}