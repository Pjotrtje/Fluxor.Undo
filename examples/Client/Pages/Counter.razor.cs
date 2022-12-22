using Fluxor;
using Microsoft.AspNetCore.Components;
using FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Store.CounterUseCase;
using FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Store;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Pages;

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
