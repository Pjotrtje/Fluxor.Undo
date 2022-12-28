namespace Fluxor.Undo;

public interface IUndoableAction
{
}

public interface IUndoableAction<TUndoable> : IUndoableAction
    where TUndoable : Undoable<TUndoable>
{
}
