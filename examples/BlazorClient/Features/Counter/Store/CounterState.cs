using Fluxor;

namespace BlazorClient.Features.Counter.Store;

[FeatureState(Name = "Counter", CreateInitialStateMethodName = nameof(CreateInitialState))]
public record CounterState(int ClickCount)
{
    public static CounterState CreateInitialState()
        => new(0);
}
