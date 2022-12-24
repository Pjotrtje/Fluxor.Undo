namespace Fluxor.Undo;

public interface IUndoableAction<TUndoable> : IUndoableAction
    where TUndoable : Undoable<TUndoable>
{
}

public interface IUndoableAction
{
}
