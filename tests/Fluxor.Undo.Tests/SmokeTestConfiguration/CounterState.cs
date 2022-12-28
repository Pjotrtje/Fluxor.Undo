#if NET7_0_OR_GREATER

namespace Fluxor.Undo.Tests.SmokeTestConfiguration;

public sealed record CounterState(int ClickCount);

[FeatureState(Name = "UndoableCounter", CreateInitialStateMethodName = nameof(CreateInitialState))]
public sealed record UndoableCounterState : Undoable<UndoableCounterState, CounterState>
{
    public static UndoableCounterState CreateInitialState()
        => new() { Present = new(0) };
};

#endif
