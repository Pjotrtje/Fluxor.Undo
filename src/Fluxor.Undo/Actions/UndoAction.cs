namespace Fluxor.Undo;

public sealed record UndoAction<TUndoable> : IUndoableAction<TUndoable>
    where TUndoable : Undoable<TUndoable>;
