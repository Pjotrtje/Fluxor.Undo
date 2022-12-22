namespace Fluxor.Undo;

/// <summary>
/// ToDo
/// </summary>
public abstract class UndoableStateReducers<TState>
    where TState : notnull
{
    /// <summary>
    /// ToDo
    /// </summary>
    [ReducerMethod]
    public virtual Undoable<TState> OnUndoAction(Undoable<TState> state, UndoAction<TState> _)
        => state.WithUndoOne();

    /// <summary>
    /// ToDo
    /// </summary>
    [ReducerMethod]
    public virtual Undoable<TState> OnJumpAction(Undoable<TState> state, JumpAction<TState> action)
        => state.WithJump(action.Amount);

    /// <summary>
    /// ToDo
    /// </summary>
    [ReducerMethod]
    public virtual Undoable<TState> OnUndoAllAction(Undoable<TState> state, UndoAllAction<TState> _)
        => state.WithUndoAll();

    /// <summary>
    /// ToDo
    /// </summary>
    [ReducerMethod]
    public virtual Undoable<TState> OnRedoAction(Undoable<TState> state, RedoAction<TState> _)
        => state.WithRedoOne();

    /// <summary>
    /// ToDo
    /// </summary>
    [ReducerMethod]
    public virtual Undoable<TState> OnRedoAllAction(Undoable<TState> state, RedoAllAction<TState> _)
        => state.WithRedoAll();

    /// <summary>
    /// ToDo
    /// </summary>
    [ReducerMethod]
    public virtual Undoable<TState> OnClearPastAndFutureAction(Undoable<TState> state, ClearPastAndFutureAction<TState> _)
        => Undoable.Create(state.Present);
}
