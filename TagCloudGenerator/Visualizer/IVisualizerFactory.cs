namespace TagsCloudVisualization;

public interface IVisualizerFactory
{
    IVisualizer Create(ICloudLayouter layouter);
}
