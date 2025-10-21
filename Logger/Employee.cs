#nullable enable
namespace Logger;

/// <summary>
/// Employee entity.
/// IEntity is implemented implicitly.
/// </summary>

public sealed record class Employee : PersonEntity
{
    public string? Department { get; init; }

    public bool Equals(Employee? other) =>
            other is not null && Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
}

