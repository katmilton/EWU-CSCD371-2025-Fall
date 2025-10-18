#nullable enable
namespace Logger;

/// <summary>
/// Book entity with Title/Author.
/// Implements IEntity implicity so Id/Name can be reached directly.
/// Name = Title.
/// </summary>

public sealed record class Book : EntityBase
{
    public required string Title { get; init; }
    public string? Author { get; init; }

    /// <inheritdoc />
    public override string Name => Title;
}
