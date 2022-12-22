using Fluxor;
using Fluxor.Undo;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.UndoableCounter.Store;

public class Reducers : UndoableStateReducers<CounterState>
{
    [ReducerMethod]
    public static Undoable<CounterState> ReduceIncrementCounterAction(Undoable<CounterState> state, IncrementCounterAction action)
        => state.WithNewPresent(p => p with
        {
            ClickCount = p.ClickCount + action.Amount,
        });
}
