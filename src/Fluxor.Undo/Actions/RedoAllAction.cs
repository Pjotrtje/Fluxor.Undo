namespace Fluxor.Undo;

public sealed record RedoAllAction<TUndoable> : IUndoableAction<TUndoable>
    where TUndoable : Undoable<TUndoable>;
