using Fluxor;

using BlazorClient.Features.Counter.Store;

using Microsoft.AspNetCore.Components;

namespace BlazorClient.Features.Counter;

public partial class Counter
{
    [Inject]
    private IState<CounterState> CounterState { get; set; } = null!;

    [Inject]
    public IDispatcher Dispatcher { get; set; } = null!;
}
