namespace Fluxor.Undo;

public sealed record JumpAction<TUndoable>(int Amount) : IUndoableAction<TUndoable>
    where TUndoable : Undoable<TUndoable>;
