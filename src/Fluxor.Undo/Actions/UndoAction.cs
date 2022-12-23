namespace Fluxor.Undo;

public sealed record UndoAction<TState> : IUndoableAction<TState>
    where TState : IUndoable;
