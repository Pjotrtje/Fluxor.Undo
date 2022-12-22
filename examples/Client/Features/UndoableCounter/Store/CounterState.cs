using Fluxor;

namespace FluxorBlazorWeb.ReduxDevToolsTutorial.Client.Features.UndoableCounter.Store;

[FeatureState(Name = "UndoableCounter")]
public class CounterState
{
    public int ClickCount { get; }

    private CounterState()
    { }

    public CounterState(int clickCount)
    {
        ClickCount = clickCount;
    }
}
