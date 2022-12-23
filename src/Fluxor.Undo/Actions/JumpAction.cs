namespace Fluxor.Undo;

public sealed record JumpAction<TState>(int Amount) : IUndoableAction<TState>
    where TState : IUndoable;
