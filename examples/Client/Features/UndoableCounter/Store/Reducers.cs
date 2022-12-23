using Fluxor;
using Fluxor.Undo;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.UndoableCounter.Store;

public class Reducers : UndoableStateReducers<UndoableCounterState>
{
    [ReducerMethod]
    public static UndoableCounterState ReduceIncrementCounterAction(UndoableCounterState state, IncrementCounterAction action)
        => state.WithNewPresent(p => p with
        {
            ClickCount = p.ClickCount + action.Amount,
        });
}
