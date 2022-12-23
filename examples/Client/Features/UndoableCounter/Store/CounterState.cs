using Fluxor;
using Fluxor.Undo;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.UndoableCounter.Store;

public record CounterState(int ClickCount);

#if NET6_0
[FeatureState(Name = "UndoableCounter", CreateInitialStateMethodName = nameof(CreateInitialState))]
public record UndoableCounterState(CounterState Present)
    : Undoable<UndoableCounterState, CounterState>(Present)
{
    public static UndoableCounterState CreateInitialState()
        => new(new CounterState(0));
};

#else
[FeatureState(Name = "UndoableCounter", CreateInitialStateMethodName = nameof(CreateInitialState))]
public record UndoableCounterState : Undoable<UndoableCounterState, CounterState>
{
    public static UndoableCounterState CreateInitialState()
        => new() { Present = new(0) };
};

#endif

//#if NET6_0
//public record UndoableCounterState(CounterState Present)
//    : Undoable<UndoableCounterState, CounterState>(Present);

//public sealed class UndoableCounterFeature : Feature<UndoableCounterState>
//{
//    public override string GetName()
//        => "UndoableCounter";

//    protected override UndoableCounterState GetInitialState()
//        => new(new CounterState(0));
//}

//#else
//public record UndoableCounterState : Undoable<UndoableCounterState, CounterState>;

//public sealed class UndoableCounterFeature : Feature<UndoableCounterState>
//{
//    public override string GetName()
//        => "UndoableCounter";

//    protected override UndoableCounterState GetInitialState()
//        => new() { Present = new(0) };
//}

//#endif
