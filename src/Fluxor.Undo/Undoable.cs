namespace Fluxor.Undo;

/// <summary>
/// ToDo
/// </summary>
public static class Undoable
{
    /// <summary>
    /// ToDo
    /// </summary>
    public static Undoable<TState> Create<TState>(TState present)
        where TState : notnull
        => new(
            Array.Empty<TState>(),
            present,
            Array.Empty<TState>());

    /// <summary>
    /// ToDo
    /// </summary>
    public static Undoable<TState> Create<TState>(IReadOnlyList<TState> status, int indexOfPresent)
        where TState : notnull
    {
        var index = GetFixedIndexOfPresent(status, indexOfPresent);
        return new(
            status.Take(index).ToList(),
            status[index],
            status.Skip(index + 1).ToList());
    }

    private static int GetFixedIndexOfPresent<TState>(IReadOnlyList<TState> status, int indexOfPresent)
    {
        if (indexOfPresent > status.Count)
        {
            return status.Count - 1;
        }

        if (indexOfPresent < 0)
        {
            return 0;
        }

        return indexOfPresent;
    }
}

/// <summary>
/// ToDo
/// </summary>
public sealed record Undoable<TState>(
    IReadOnlyList<TState> Past,
    TState Present,
    IReadOnlyList<TState> Future)
    where TState : notnull
{
    /// <summary>
    /// ToDo
    /// </summary>
    public TimeTravelInfo TimeTravelInfo { get; } = new(Past.Any(), Future.Any());

    /// <summary>
    /// ToDo
    /// </summary>
    public Undoable<TState> WithNewPresent(TState present)
        => WithNewPresent(_ => present);

    /// <summary>
    /// ToDo
    /// </summary>
    public Undoable<TState> WithNewPresent(Func<TState, TState> map)
    {
        var newPresent = map(Present);

        return newPresent.Equals(Present)
            ? this
            : new(
                Past.Append(Present).ToList(),
                newPresent,
                Array.Empty<TState>());
    }

    /// <summary>
    /// ToDo
    /// </summary>
    public Undoable<TState> WithUndoAll()
        => WithJump(-Past.Count);

    /// <summary>
    /// ToDo
    /// </summary>
    public Undoable<TState> WithUndoOne()
        => WithJump(-1);

    /// <summary>
    /// ToDo
    /// </summary>
    public Undoable<TState> WithRedoOne()
        => WithJump(1);

    /// <summary>
    /// ToDo
    /// </summary>
    public Undoable<TState> WithRedoAll()
        => WithJump(Future.Count);

    /// <summary>
    /// ToDo
    /// </summary>
    public Undoable<TState> WithJump(int amount)
    {
        var fixedAmount = GetFixedAmount(amount);
        return fixedAmount switch
        {
            0 => this,
            < 0 => WithJumpToPast(-fixedAmount),
            > 0 => WithJumpToFuture(fixedAmount),
        };
    }
    private Undoable<TState> WithJumpToPast(int amount)
    {
        var past = Past
            .SkipLast(amount)
            .ToList();

        var pastToFuture = Past.TakeLast(amount - 1);
        var future = pastToFuture
            .Append(Present)
            .Concat(Future)
            .ToList();

        return new(
            past,
            Past[^amount],
            future);
    }

    private Undoable<TState> WithJumpToFuture(int amount)
    {
        var futureToPast = Future.Take(amount - 1);
        var past = Past
            .Append(Present)
            .Concat(futureToPast)
            .ToList();

        var future = Future
            .Skip(amount)
            .ToList();

        return new(
            past,
            Future[amount - 1],
            future);
    }

    private int GetFixedAmount(int amount)
    {
        if (amount > Future.Count)
        {
            return Future.Count;
        }

        if (-amount > Past.Count)
        {
            return -Past.Count;
        }

        return amount;
    }
}
