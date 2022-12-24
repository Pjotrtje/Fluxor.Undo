namespace Fluxor.Undo;

public abstract class UndoableStateReducers<TUndoable>
    where TUndoable : Undoable<TUndoable>
{
    [ReducerMethod]
    public static TUndoable OnUndoAction(TUndoable state, UndoAction<TUndoable> _)
        => state.WithUndoOne();

    [ReducerMethod]
    public static TUndoable OnJumpAction(TUndoable state, JumpAction<TUndoable> action)
        => state.WithJump(action.Amount);

    [ReducerMethod]
    public static TUndoable OnUndoAllAction(TUndoable state, UndoAllAction<TUndoable> _)
        => state.WithUndoAll();

    [ReducerMethod]
    public static TUndoable OnRedoAction(TUndoable state, RedoAction<TUndoable> _)
        => state.WithRedoOne();

    [ReducerMethod]
    public static TUndoable OnRedoAllAction(TUndoable state, RedoAllAction<TUndoable> _)
        => state.WithRedoAll();
}
