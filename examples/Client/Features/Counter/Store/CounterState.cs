using Fluxor;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.Counter.Store;

[FeatureState(Name = "Counter")]
public record CounterState(int ClickCount)
{
    private CounterState() : this(0)
    { }
}
