using Fluxor;
using Fluxor.Undo;

namespace BlazorClient.Features.UndoableCounterWithPersistence.Store;

public record CounterState(int ClickCount);

[FeatureState(Name = "UndoableCounterWithPersistence", CreateInitialStateMethodName = nameof(CreateInitialState))]
public record UndoableCounterState : Undoable<UndoableCounterState, CounterState>
{
    public required CounterState Persisted { get; init; }

    public bool IsPersisted => Persisted == Present;

    public static UndoableCounterState CreateInitialState()
        => new()
        {
            Persisted = new(0),
            Present = new(0),
        };
};
