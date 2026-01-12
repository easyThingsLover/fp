using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudGenerator.FileConverter.Tests;

[TestFixture]
public class BoringWordFilterTests
{
    [Test]
    public void Filter_RemovesShortHighFrequencyAndLowCount()
    {
        var raw = new Dictionary<string, int>()
        {
            {"a", 10},
            {"hello", 1},
            {"word", 2}
        };
        var totalWords = 13;

        var filter = new BoringWordFilter();
        var result = filter.Filter(raw, totalWords, null);

        result.Should().ContainKey("word");
        result["word"].Should().Be(2);
        result.Should().NotContainKey("a");
        result.Should().NotContainKey("hello");
    }

    [Test]
    public void Filter_RemovesEmptyOrNullWord()
    {
        var raw = new Dictionary<string, int>()
        {
            {"", 5},
            {"good", 3}
        };

        var filter = new BoringWordFilter();
        var result = filter.Filter(raw, 8, null);

        result.Should().ContainKey("good");
        result["good"].Should().Be(3);
        result.Should().NotContainKey("");
    }

    [Test]
    public void Filter_RemovesShortWordAtMinimalCount()
    {
        var raw = new Dictionary<string, int>()
        {
            {"it", 2},
            {"content", 10}
        };

        var totalWords = 20;

        var filter = new BoringWordFilter();
        var result = filter.Filter(raw, totalWords, null);
        
        result.Should().NotContainKey("it");
        result.Should().ContainKey("content");
    }
    [Test]
    public void Filter_RespectsProvidedBlacklist()
    {
        var raw = new Dictionary<string, int>()
        {
            {"skipme", 10},
            {"keep", 5}
        };

        var filter = new BoringWordFilter();
        var blacklist = new HashSet<string>() { "skipme" };
        var result = filter.Filter(raw, 15, blacklist);

        result.Should().ContainKey("keep");
        result.Should().NotContainKey("skipme");
    }
}