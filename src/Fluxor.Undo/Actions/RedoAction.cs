namespace Fluxor.Undo;

public sealed record RedoAction<TUndoable> : IUndoableAction<TUndoable>
    where TUndoable : Undoable<TUndoable>;
