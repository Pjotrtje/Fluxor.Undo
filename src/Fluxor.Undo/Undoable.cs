namespace Fluxor.Undo;

public abstract record Undoable<TUndoable>
{
    private protected Undoable()
    {
    }

    public abstract TUndoable WithUndoAll();

    public abstract TUndoable WithUndoOne();

    public abstract TUndoable WithRedoOne();

    public abstract TUndoable WithRedoAll();

    public abstract TUndoable WithJump(int amount);
}

#if NET7_0_OR_GREATER
public record Undoable<TUndoable, TState> : Undoable<TUndoable>
    where TUndoable : Undoable<TUndoable, TState>
    where TState : notnull
{
    public required TState Present { get; init; }
#else

public record Undoable<TUndoable, TState>(TState Present) : Undoable<TUndoable>
    where TUndoable : Undoable<TUndoable, TState>
    where TState : notnull
{
#endif

    public IReadOnlyList<TState> Past { get; init; } = Array.Empty<TState>();

    public IReadOnlyList<TState> Future { get; init; } = Array.Empty<TState>();

    public bool HasPast => Past.Any();

    public bool HasNoPast => !HasPast;

    public bool HasFuture => Future.Any();

    public bool HasNoFuture => !HasFuture;

    public TUndoable WithNewPresent(TState present)
        => WithNewPresent(_ => present);

    public TUndoable WithNewPresent(Func<TState, TState> map)
    {
        var newPresent = map(Present);

        return newPresent.Equals(Present)
            ? Self
            : Self with
            {
                Past = Past.Append(Present).ToList(),
                Present = newPresent,
                Future = Array.Empty<TState>(),
            };
    }

    public TUndoable WithInlineEditedPresent(TState present)
        => WithInlineEditedPresent(_ => present);

    public TUndoable WithInlineEditedPresent(Func<TState, TState> map)
    {
        var newPresent = map(Present);

        return newPresent.Equals(Present)
            ? Self
            : Self with
            {
                Past = Past,
                Present = newPresent,
                Future = Array.Empty<TState>(),
            };
    }

    /// <inheritdoc/>
    public override TUndoable WithUndoAll()
        => WithJump(-Past.Count);

    /// <inheritdoc/>
    public override TUndoable WithUndoOne()
        => WithJump(-1);

    /// <inheritdoc/>
    public override TUndoable WithRedoOne()
        => WithJump(1);

    /// <inheritdoc/>
    public override TUndoable WithRedoAll() => WithJump(Future.Count);

    /// <inheritdoc/>
    public override TUndoable WithJump(int amount)
    {
        var fixedAmount = GetFixedAmount(amount);
        return fixedAmount switch
        {
            0 => Self,
            < 0 => WithJumpToPast(-fixedAmount),
            > 0 => WithJumpToFuture(fixedAmount),
        };
    }

    private TUndoable Self
        => (TUndoable)this;

    private TUndoable WithJumpToPast(int amount)
    {
        var past = Past
            .SkipLast(amount)
            .ToList();

        var pastToFuture = Past.TakeLast(amount - 1);
        var future = pastToFuture
            .Append(Present)
            .Concat(Future)
            .ToList();

        return Self with
        {
            Past = past,
            Present = Past[^amount],
            Future = future,
        };
    }

    private TUndoable WithJumpToFuture(int amount)
    {
        var futureToPast = Future.Take(amount - 1);
        var past = Past
            .Append(Present)
            .Concat(futureToPast)
            .ToList();

        var future = Future
            .Skip(amount)
            .ToList();

        return Self with
        {
            Past = past,
            Present = Future[amount - 1],
            Future = future,
        };
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
