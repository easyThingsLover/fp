namespace TagsCloudVisualization;

public class VisualizerFactory : IVisualizerFactory
{
    public IVisualizer Create(ICloudLayouter layouter) => new Visualizer(layouter);
}
