namespace TagsCloudVisualization;

public interface IPointGeneratorFactory
{
    IPointGenerator Create(int generatorNumber);
}