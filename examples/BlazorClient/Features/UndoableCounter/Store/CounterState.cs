using Fluxor;
using Fluxor.Undo;

namespace BlazorClient.Features.UndoableCounter.Store;

public record CounterState(int ClickCount);

[FeatureState(Name = "UndoableCounter", CreateInitialStateMethodName = nameof(CreateInitialState))]
public record UndoableCounterState : Undoable<UndoableCounterState, CounterState>
{
    public static UndoableCounterState CreateInitialState()
        => new() { Present = new(0) };
}
