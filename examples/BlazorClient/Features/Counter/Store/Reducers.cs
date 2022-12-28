using Fluxor;

namespace BlazorClient.Features.Counter.Store;

public static class Reducers
{
    [ReducerMethod]
    public static CounterState ReduceIncrementCounterAction(CounterState state, IncrementCounterAction action)
        => state with
        {
            ClickCount = state.ClickCount + action.Amount,
        };
}
