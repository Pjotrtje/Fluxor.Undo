using Fluxor;

using BlazorClient.Features.UndoableCounter.Store;

using Microsoft.AspNetCore.Components;

namespace BlazorClient.Features.UndoableCounter;

public partial class UndoableCounter
{
    [Inject]
    private IState<UndoableCounterState> UndoableCounterState { get; set; } = null!;

    [Inject]
    public IDispatcher Dispatcher { get; set; } = null!;
}
