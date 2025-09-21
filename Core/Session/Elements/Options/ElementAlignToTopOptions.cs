namespace Core.Session.Elements.Options;


public sealed class ElementAlignToTopOptions
{
    public ElementAlignToTopBehavior Behavior { get; init; } = ElementAlignToTopBehavior.Auto;
    public ElementAlignToTopAlignment Alignment { get; init; } = ElementAlignToTopAlignment.Start;
}

public enum ElementAlignToTopBehavior
{
    Smooth,
    Instant,
    Auto
}

public enum ElementAlignToTopAlignment
{
    Start,
    End,
    Center,
    Nearest
}