namespace Fluxor.Undo;

public sealed record UndoAllAction<TState> : IUndoableAction<TState>
    where TState : IUndoable;
