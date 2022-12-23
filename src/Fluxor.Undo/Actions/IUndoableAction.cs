namespace Fluxor.Undo;

public interface IUndoableAction<TState> : IUndoableAction
    where TState : IUndoable
{
}

public interface IUndoableAction
{
}
