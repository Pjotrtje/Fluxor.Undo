using Fluxor;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.Counter.Store;

[FeatureState(Name = "Counter", CreateInitialStateMethodName = nameof(CreateInitialState))]
public record CounterState(int ClickCount)
{
    public static CounterState CreateInitialState()
        => new(0);
}

//public record CounterState(int ClickCount);

//public sealed class CounterFeature : Feature<CounterState>
//{
//    public override string GetName()
//        => "Counter";

//    protected override CounterState GetInitialState()
//        => new(0);
//}
