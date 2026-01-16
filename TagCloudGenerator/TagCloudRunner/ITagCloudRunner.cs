using ResultOf;

namespace TagsCloudVisualization;

public interface ITagCloudRunner
{
    Result<None> Run(TagCloudOptions options);
}
