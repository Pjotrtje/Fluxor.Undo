namespace Fluxor.Undo;

/// <summary>
/// ToDo
/// </summary>
public abstract class UndoableFeature<TState> : Feature<Undoable<TState>>
    where TState : notnull
{
}
