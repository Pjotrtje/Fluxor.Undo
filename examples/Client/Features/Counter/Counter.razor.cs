using Fluxor;

using FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.Counter.Store;

using Microsoft.AspNetCore.Components;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.Counter;

public partial class Counter
{
    [Inject]
    private IState<CounterState> CounterState { get; set; } = null!;

    [Inject]
    public IDispatcher Dispatcher { get; set; } = null!;

    private void IncrementCount()
    {
        var action = new IncrementCounterAction();
        Dispatcher.Dispatch(action);
    }
}
