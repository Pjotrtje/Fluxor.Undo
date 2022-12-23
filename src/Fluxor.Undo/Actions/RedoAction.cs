namespace Fluxor.Undo;

public sealed record RedoAction<TState> : IUndoableAction<TState>
    where TState : IUndoable;
