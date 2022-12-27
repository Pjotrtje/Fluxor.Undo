using Fluxor;
using Fluxor.Undo;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.UndoableCounterWithPersistence.Store;

public record CounterState(int ClickCount);

#if NET7_0_OR_GREATER
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

#else
[FeatureState(Name = "UndoableCounterWithPersistence", CreateInitialStateMethodName = nameof(CreateInitialState))]
public record UndoableCounterState(CounterState Present, CounterState Persisted)
    : Undoable<UndoableCounterState, CounterState>(Present)
{
    public bool IsPersisted => Persisted == Present;

    public static UndoableCounterState CreateInitialState()
        => new(new CounterState(0), new CounterState(0));
};

#endif
