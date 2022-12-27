#if NET7_0_OR_GREATER

namespace Fluxor.Undo.Tests.SmokeTestConfiguration;

public record CounterState(int ClickCount);

[FeatureState(Name = "UndoableCounter", CreateInitialStateMethodName = nameof(CreateInitialState))]
public record UndoableCounterState : Undoable<UndoableCounterState, CounterState>
{
    public static UndoableCounterState CreateInitialState()
        => new() { Present = new(0) };
};

#endif
