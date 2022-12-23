namespace Fluxor.Undo;

public abstract class UndoableStateReducers<TUndoableState>
    where TUndoableState : Undoable<TUndoableState>
{
    [ReducerMethod]
    public TUndoableState OnUndoAction(TUndoableState state, UndoAction<TUndoableState> _)
        => state.WithUndoOne();

    [ReducerMethod]
    public TUndoableState OnJumpAction(TUndoableState state, JumpAction<TUndoableState> action)
        => state.WithJump(action.Amount);

    [ReducerMethod]
    public TUndoableState OnUndoAllAction(TUndoableState state, UndoAllAction<TUndoableState> _)
        => state.WithUndoAll();

    [ReducerMethod]
    public TUndoableState OnRedoAction(TUndoableState state, RedoAction<TUndoableState> _)
        => state.WithRedoOne();

    [ReducerMethod]
    public TUndoableState OnRedoAllAction(TUndoableState state, RedoAllAction<TUndoableState> _)
        => state.WithRedoAll();
}
