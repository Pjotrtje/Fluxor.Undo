namespace Fluxor.Undo;

/// <summary>
/// ToDo
/// </summary>
public sealed record JumpAction<TState>(int Amount) : IUndoableAction<TState>;
