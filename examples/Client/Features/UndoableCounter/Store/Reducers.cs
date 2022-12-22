using Fluxor;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.UndoableCounter.Store;

public static class Reducers
{
    [ReducerMethod]
    public static CounterState ReduceIncrementCounterAction(CounterState state, IncrementCounterAction action) =>
        new(clickCount: state.ClickCount + 1);
}
