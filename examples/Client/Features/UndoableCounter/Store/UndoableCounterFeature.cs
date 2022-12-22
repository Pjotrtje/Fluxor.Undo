using Fluxor.Undo;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.UndoableCounter.Store;

public sealed class UndoableCounterFeature : UndoableFeature<CounterState>
{
    public override string GetName()
        => "UndoableCounter";

    protected override Undoable<CounterState> GetInitialState()
        => Undoable.Create(new CounterState(0));
}
