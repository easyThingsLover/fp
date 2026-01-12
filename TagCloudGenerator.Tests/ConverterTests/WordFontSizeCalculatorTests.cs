using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudGenerator.FileConverter.Tests;

[TestFixture]
public class WordFontSizeCalculatorTests
{
    [Test]
    public void CalculateFontSizes_NormalScaling_ReturnsExpectedSizes()
    {
        var calculator = new WordFontSizeCalculator();
        var input = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            {"a", 2},
            {"b", 1}
        };

        var result = calculator.CalculateFontSizes(input);

        result.Should().ContainKey("a");
        result["a"].Should().Be(72f);
        result.Should().ContainKey("b");
        result["b"].Should().Be(40f);
    }

    [Test]
    public void CalculateFontSizes_EmptyDictionary_ThrowsInvalidOperationException()
    {
        var calculator = new WordFontSizeCalculator();
        var empty = new Dictionary<string, int>();

        FluentActions.Invoking(() => calculator.CalculateFontSizes(empty))
            .Should().Throw<InvalidOperationException>();
    }
}