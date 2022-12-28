namespace Fluxor.Undo;

public sealed record UndoAllAction<TUndoable> : IUndoableAction<TUndoable>
    where TUndoable : Undoable<TUndoable>;
