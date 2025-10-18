#nullable enable
namespace Logger;

/// <summary>
/// Student entity.
/// IEntity is implemented implicitly.
/// </summary>
public sealed record class Student : PersonEntity
{
    public string? StudentNumber { get; init; }
}

