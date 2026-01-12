namespace TagsCloudVisualization;

public class PointGeneratorFactory : IPointGeneratorFactory
{
    private readonly IReadOnlyDictionary<int, IPointGenerator> generators;

    public PointGeneratorFactory()
    {
        generators = new Dictionary<int, IPointGenerator>()
        {
            [1] = new SpiralPointGenerator(),
            [2] = new SquarePointGenerator(),
        };
    }

    public IPointGenerator Create(int generatorNumber)
    {
        return generators[generatorNumber];
    }
}