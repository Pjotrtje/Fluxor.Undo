#if NET7_0_OR_GREATER

namespace Fluxor.Undo.Tests.SmokeTestConfiguration;

public class Reducers : UndoableStateReducers<UndoableCounterState>
{
    [ReducerMethod]
    public static UndoableCounterState ReduceIncrementCounterAction(UndoableCounterState state, IncrementCounterAction action)
        => state.WithNewPresent(p => p with
        {
            ClickCount = p.ClickCount + action.Amount,
        });
}

#endif
