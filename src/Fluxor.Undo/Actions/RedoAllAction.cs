namespace Fluxor.Undo;

public sealed record RedoAllAction<TState> : IUndoableAction<TState>
    where TState : IUndoable;
