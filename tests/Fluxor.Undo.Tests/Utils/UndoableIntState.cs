namespace Fluxor.Undo.Tests.Utils;

#if NET7_0_OR_GREATER

public sealed record UndoableIntState : Undoable<UndoableIntState, int>;

#else

public sealed record UndoableIntState(int Present)
    : Undoable<UndoableIntState, int>(Present);

#endif
