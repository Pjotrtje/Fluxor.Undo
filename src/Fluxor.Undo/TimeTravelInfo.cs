namespace Fluxor.Undo;

/// <summary>
/// ToDo
/// </summary>
/// <param name="HasPast">HHHH</param>
/// <param name="HasFuture"></param>
public readonly record struct TimeTravelInfo(
    bool HasPast,
    bool HasFuture)
{
    /// <summary>
    /// ToDo
    /// </summary>
    public bool HasNoPast => !HasPast;

    /// <summary>
    /// ToDo
    /// </summary>
    public bool HasNoFuture => !HasFuture;
}
