namespace Fluxor.Undo.Tests.Utils;

#if NET7_0_OR_GREATER

//public record UndoableIntState : Undoable<UndoableIntState, int>;

public record UndoableIntState : Undoable<UndoableIntState, int>;

#else

public record UndoableIntState(int Present)
    : Undoable<UndoableIntState, int>(Present);

#endif
