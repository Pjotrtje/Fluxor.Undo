using Fluxor;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.Counter.Store;

//[FeatureState(Name = "Counter")]
//public record CounterState(int ClickCount)
//{
//    private CounterState() : this(0)
//    { }
//}

public record CounterState(int ClickCount);

public sealed class CounterFeature : Feature<CounterState>
{
    public override string GetName()
        => "Counter";

    protected override CounterState GetInitialState()
        => new(0);
}
