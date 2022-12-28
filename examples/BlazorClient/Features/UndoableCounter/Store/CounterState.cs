using Fluxor;
using Fluxor.Undo;

namespace BlazorClient.Features.UndoableCounter.Store;

public sealed record CounterState(int ClickCount);

[FeatureState(Name = "UndoableCounter", CreateInitialStateMethodName = nameof(CreateInitialState))]
public sealed record UndoableCounterState : Undoable<UndoableCounterState, CounterState>
{
    public static UndoableCounterState CreateInitialState()
        => new() { Present = new(0) };
}
