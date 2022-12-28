using Fluxor;
using Fluxor.Undo;

namespace BlazorClient.Features.UndoableCounter.Store;

public class Reducers : UndoableReducers<UndoableCounterState>
{
    [ReducerMethod]
    public static UndoableCounterState ReduceIncrementCounterAction(UndoableCounterState state, IncrementCounterAction action)
        => state.WithNewPresent(p => p with
        {
            ClickCount = p.ClickCount + action.Amount,
        });
}
