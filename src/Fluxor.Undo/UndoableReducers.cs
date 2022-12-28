namespace Fluxor.Undo;

public abstract class UndoableReducers<TUndoable>
    where TUndoable : Undoable<TUndoable>
{
    [ReducerMethod]
    public static TUndoable ReduceUndoAction(TUndoable state, UndoAction<TUndoable> _)
        => state.WithUndoOne();

    [ReducerMethod]
    public static TUndoable ReduceJumpAction(TUndoable state, JumpAction<TUndoable> action)
        => state.WithJump(action.Amount);

    [ReducerMethod]
    public static TUndoable ReduceUndoAllAction(TUndoable state, UndoAllAction<TUndoable> _)
        => state.WithUndoAll();

    [ReducerMethod]
    public static TUndoable ReduceRedoAction(TUndoable state, RedoAction<TUndoable> _)
        => state.WithRedoOne();

    [ReducerMethod]
    public static TUndoable ReduceRedoAllAction(TUndoable state, RedoAllAction<TUndoable> _)
        => state.WithRedoAll();
}
