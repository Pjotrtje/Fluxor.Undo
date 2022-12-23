namespace Fluxor.Undo;

public interface IUndoable
{ }

public abstract record Undoable<TSelf> : IUndoable
{
    private protected Undoable()
    {
    }

    public abstract TSelf WithUndoAll();

    public abstract TSelf WithUndoOne();

    public abstract TSelf WithRedoOne();

    public abstract TSelf WithRedoAll();

    public abstract TSelf WithJump(int amount);
}

#if NET6_0
public record Undoable<TSelf, TState>(TState Present) : Undoable<TSelf>
    where TSelf : Undoable<TSelf, TState>
    where TState : notnull
{
#else
public record Undoable<TSelf, TState> : Undoable<TSelf>
    where TSelf : Undoable<TSelf, TState>
    where TState : notnull
{
    public required TState Present { get; init; }
#endif

    public IReadOnlyList<TState> Past { get; init; } = Array.Empty<TState>();

    public IReadOnlyList<TState> Future { get; init; } = Array.Empty<TState>();

    public bool HasPast => Past.Any();

    public bool HasNoPast => !HasPast;

    public bool HasFuture => Future.Any();

    public bool HasNoFuture => !HasFuture;

    public TSelf WithNewPresent(TState present)
        => WithNewPresent(_ => present);

    public TSelf WithNewPresent(Func<TState, TState> map)
    {
        var newPresent = map(Present);

        var returnValue = newPresent.Equals(Present)
            ? this
            : this with
            {
                Past = Past.Append(Present).ToList(),
                Present = newPresent,
                Future = Array.Empty<TState>(),
            };

        return (TSelf)returnValue;
    }

    public TSelf WithInlineEditedPresent(TState present)
        => WithInlineEditedPresent(_ => present);

    public TSelf WithInlineEditedPresent(Func<TState, TState> map)
    {
        var newPresent = map(Present);

        var returnValue = newPresent.Equals(Present)
            ? this
            : this with
            {
                Past = Past,
                Present = newPresent,
                Future = Array.Empty<TState>(),
            };

        return (TSelf)returnValue;
    }

    /// <inheritdoc/>
    public override TSelf WithUndoAll()
        => WithJump(-Past.Count);

    /// <inheritdoc/>
    public override TSelf WithUndoOne()
        => WithJump(-1);

    /// <inheritdoc/>
    public override TSelf WithRedoOne()
        => WithJump(1);

    /// <inheritdoc/>
    public override TSelf WithRedoAll() => WithJump(Future.Count);

    /// <inheritdoc/>
    public override TSelf WithJump(int amount)
    {
        var fixedAmount = GetFixedAmount(amount);
        var returnValue = fixedAmount switch
        {
            0 => this,
            < 0 => WithJumpToPast(-fixedAmount),
            > 0 => WithJumpToFuture(fixedAmount),
        };
        return (TSelf)returnValue;
    }

    private IUndoable WithJumpToPast(int amount)
    {
        var past = Past
            .SkipLast(amount)
            .ToList();

        var pastToFuture = Past.TakeLast(amount - 1);
        var future = pastToFuture
            .Append(Present)
            .Concat(Future)
            .ToList();

        return this with
        {
            Past = past,
            Present = Past[^amount],
            Future = future,
        };
    }

    private IUndoable WithJumpToFuture(int amount)
    {
        var futureToPast = Future.Take(amount - 1);
        var past = Past
            .Append(Present)
            .Concat(futureToPast)
            .ToList();

        var future = Future
            .Skip(amount)
            .ToList();

        return this with
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
