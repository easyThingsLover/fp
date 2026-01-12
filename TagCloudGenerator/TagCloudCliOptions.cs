using CommandLine;

public class TagCloudCliOptions
{
    [Value(0, MetaName = "input", Required = true, HelpText = "Input text file.")]
    public string InputFile { get; set; }

    [Value(1, MetaName = "output", Required = true, HelpText = "Output image file.")]
    public string OutputFile { get; set;}

    [Value(2, MetaName = "width", Required = true, HelpText = "Image width.")]
    public int ImageWidth { get; set;}

    [Value(3, MetaName = "height", Required = true, HelpText = "Image height.")]
    public int ImageHeight { get; set;}

    [Value(4, MetaName = "font", Required = true, HelpText = "Font name.")]
    public string FontName { get; set;}

    [Value(5, MetaName = "textColor", Required = true, HelpText = "Text color name.")]
    public string TextColorName { get; set;}

    [Value(6, MetaName = "backgroundColor", Required = true, HelpText = "Background color name.")]
    public string BackgroundColorName { get; set;}
    
    [Value(7, MetaName = "blacklist", Required = false, HelpText = "Blacklist file path.")]
    public string BlacklistFile { get; set;}
}