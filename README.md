# Fluxor.Undo
![Icon](https://raw.githubusercontent.com/Pjotrtje/Fluxor.Undo/main/docs/icon-128x128.png) 

Fluxor.Undo is a library to add redo/undo functionality to [Fluxor](https://github.com/mrpmorris/Fluxor). 

![Azure DevOps builds (branch)](https://img.shields.io/azure-devops/build/Pjotrtje/PvS/21/main)

## Goal
The aim of Fluxor.Undo is removing the hassle of implementing your own undo/redo functionality. The idea is inspired by [redux-undo](https://github.com/omnidan/redux-undo) although the implementation is completely different.


## Installation
You can download the latest release / pre-release NuGet package from nuget:

 | Package: |  |  | 
 | :--- | --- | --- |
 | Fluxor.Undo | [![Fluxor.Undo on NuGet](https://img.shields.io/nuget/v/Fluxor.Undo.svg)](https://www.nuget.org/packages/Fluxor.Undo) | [![Fluxor.Undo downloads on NuGet](https://img.shields.io/nuget/dt/Fluxor.Undo.svg)](https://www.nuget.org/packages/Fluxor.Undo) |
 

## Setup undoable state
Steps to change your regular state to an undoable state:

**1) Change your Feature to an Undoable feature**

Change your state with FeatureStateAtrribute

```csharp
[FeatureState(Name = "Counter", CreateInitialStateMethodName = nameof(CreateInitialState))]
public record CounterState(int ClickCount)
{
    public static CounterState CreateInitialState()
        => new(0);
}
```

to

```csharp
public record CounterState(int ClickCount);

[FeatureState(Name = "Counter", CreateInitialStateMethodName = nameof(CreateInitialState))]
public record UndoableCounterState : Undoable<UndoableCounterState, CounterState>
{
    public static UndoableCounterState CreateInitialState()
        => new() { Present = new(0) };
}

// Or when net6:
public record CounterState(int ClickCount);

[FeatureState(Name = "Counter", CreateInitialStateMethodName = nameof(CreateInitialState))]
public record UndoableCounterState(CounterState Present) : Undoable<UndoableCounterState, CounterState>(Present)
{
    public static UndoableCounterState CreateInitialState()
        => new(new CounterState(0));
};
```

or state with generic Feature

```csharp
public record CounterState(int ClickCount);

public sealed class CounterFeature : Feature<CounterState>
{
    public override string GetName()
        => "Counter";

    protected override CounterState GetInitialState()
        => new(0);
}
```

to


```csharp
public record CounterState(int ClickCount);
public record UndoableCounterState : Undoable<UndoableCounterState, CounterState>;

public sealed class UndoableCounterFeature : Feature<UndoableCounterState>
{
    public override string GetName()
        => "UndoableCounter";

    protected override UndoableCounterState GetInitialState()
        => new() { Present = new(0) };
}

// Or when net6:
public record CounterState(int ClickCount);
public record UndoableCounterState(CounterState Present) : Undoable<UndoableCounterState, CounterState>(Present);

public sealed class UndoableCounterFeature : Feature<UndoableCounterState>
{
    public override string GetName()
        => "UndoableCounter";

    protected override UndoableCounterState GetInitialState()
        => new(new CounterState(0));
}
```

**2) Update your reducer**
Change your reducer from
```csharp
public static class Reducers
{
    [ReducerMethod]
    public static CounterState ReduceIncrementCounterAction(CounterState state, IncrementCounterAction action)
        => state with
        {
            ClickCount = state.ClickCount + action.Amount,
        };
}
```

to


```csharp
public class Reducers : UndoableStateReducers<UndoableCounterState>
{
    [ReducerMethod]
    public static UndoableCounterState ReduceIncrementCounterAction(UndoableCounterState state, IncrementCounterAction action)
        => state.WithNewPresent(p => p with
        {
            ClickCount = p.ClickCount + action.Amount,
        });
}
```

**3) Update your injected IState properties**
Change setting of properties in your Razor pages from
```csharp
    [Inject]
    private IState<CounterState> CounterState { get; set; } = null!;
```

to

```csharp
    [Inject]
    private IState<UndoableCounterState> UndoableCounterState { get; set; } = null!;
```

**4) Update usages of your state**
Change usage in your Razor pages from
```cshtml 
<p>Current count: @CounterState.Value.ClickCount</p>
```

to

```cshtml
<p>Current count: @UndoableCounterState.Value.Present.ClickCount</p>
```

**5) Add some navigation buttons**
```cshtml
<button class="btn btn-secondary" @onclick=@(() => Dispatcher.Dispatch(new UndoAllAction<UndoableCounterState>())) disabled="@UndoableCounterState.Value.HasNoPast">&lt;&lt;</button>
<button class="btn btn-secondary" @onclick=@(() => Dispatcher.Dispatch(new UndoAction<UndoableCounterState>())) disabled="@UndoableCounterState.Value.HasNoPast">&lt;</button>
<button class="btn btn-secondary" @onclick=@(() => Dispatcher.Dispatch(new RedoAction<UndoableCounterState>())) disabled="@UndoableCounterState.Value.HasNoFuture">&gt;</button>
<button class="btn btn-secondary" @onclick=@(() => Dispatcher.Dispatch(new RedoAllAction<UndoableCounterState>())) disabled="@UndoableCounterState.Value.HasNoFuture">&gt;&gt;</button>
```

Also see example project in solution. Here both the Fluxor counter as Fluxor.Undo counter are implemented.

## Tips
1) When you are allowing undo/redo, the undo/redo is done on the client side. So make sure that user knows that undo-ing does not alter data on server. There is a basic implementation in the example project in solution; page: Fluxor.Undo (Persist). Can be used as inspiration!
2) If you are using net6; upgrade to net7 so you can use the parameterless ctors and use the required properties :).

## Release notes
See the [Releases page](https://github.com/Pjotrtje/Fluxor.Undo/releases/).

## Versioning
Fluxor.Undo follows [Semantic Versioning 2.0.0](http://semver.org/spec/v2.0.0.html) for the releases published to [nuget.org](https://www.nuget.org/).
