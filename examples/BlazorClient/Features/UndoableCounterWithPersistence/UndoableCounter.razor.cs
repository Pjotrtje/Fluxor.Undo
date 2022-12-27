using Fluxor;

using BlazorClient.Features.UndoableCounterWithPersistence.Store;

using Microsoft.AspNetCore.Components;

namespace BlazorClient.Features.UndoableCounterWithPersistence;

public partial class UndoableCounter
{
    [Inject]
    private IState<UndoableCounterState> UndoableCounterState { get; set; } = null!;

    [Inject]
    public IDispatcher Dispatcher { get; set; } = null!;
}
