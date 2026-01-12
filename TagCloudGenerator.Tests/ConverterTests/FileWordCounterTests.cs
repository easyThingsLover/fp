using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudGenerator.FileConverter.Tests;

[TestFixture]
public class FileWordCounterTests
{
    private string CreateTempFile(IEnumerable<string> lines)
    {
        var path = Path.GetTempFileName();
        File.WriteAllLines(path, lines);
        return path;
    }

    [Test]
    public void CountWords_NormalCountsAndTotal()
    {
        var lines = new[] { "Hello", "world", "hello" };
        var path = CreateTempFile(lines);

        var counter = new FileWordCounter();
        var counts = counter.CountWords(path, out var total);

        total.Should().Be(3);
        counts.Should().ContainKey("hello");
        counts["hello"].Should().Be(2);
        counts.Should().ContainKey("world");
        counts["world"].Should().Be(1);

        File.Delete(path);
    }

    [Test]
    public void CountWords_IgnoresNonLettersAndWhitespace()
    {
        var lines = new[] { "", "123", "abc!", "Good" };
        var path = CreateTempFile(lines);

        var counter = new FileWordCounter();
        var counts = counter.CountWords(path, out var total);

        total.Should().Be(1);
        counts.Should().ContainKey("good");
        counts["good"].Should().Be(1);
        counts.Should().NotContainKey("123");
        counts.Should().NotContainKey("abc!");

        File.Delete(path);
    }

    [Test]
    public void CountWords_EmptyFile_ReturnsEmptyAndZero()
    {
        var path = CreateTempFile(Array.Empty<string>());

        var counter = new FileWordCounter();
        var counts = counter.CountWords(path, out var total);

        total.Should().Be(0);
        counts.Should().BeEmpty();

        File.Delete(path);
    }

    [Test]
    public void CountWords_IgnoresLinesWithMultipleWords()
    {
        var lines = new[] { "hello world", "ok",  "hello world" };
        var path = CreateTempFile(lines);

        var counter = new FileWordCounter();
        var counts = counter.CountWords(path, out var total);

        total.Should().Be(1);
        counts.Should().ContainKey("ok");
        counts["ok"].Should().Be(1);
        counts.Should().NotContainKey("hello world");

        File.Delete(path);
    }
    
    [Test]
    public void CountWords_CountWordsWithSpecialSymbols()
    {
        var lines = new[] { "can't", "co-owner",  "isn't" };
        var path = CreateTempFile(lines);

        var counter = new FileWordCounter();
        var counts = counter.CountWords(path, out var total);

        total.Should().Be(3);
        counts.Should().ContainKey("can't");
        counts["can't"].Should().Be(1);
        counts.Should().ContainKey("co-owner");
        counts["co-owner"].Should().Be(1);
        counts.Should().ContainKey("isn't");
        counts["isn't"].Should().Be(1);

        File.Delete(path);
    }
}