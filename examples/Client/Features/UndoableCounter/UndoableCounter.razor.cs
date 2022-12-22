using Fluxor;
using Fluxor.Undo;

using FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.UndoableCounter.Store;

using Microsoft.AspNetCore.Components;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.UndoableCounter;

public partial class UndoableCounter
{
    [Inject]
    private IState<Undoable<CounterState>> UndoableCounterState { get; set; } = null!;

    [Inject]
    public IDispatcher Dispatcher { get; set; } = null!;
}
