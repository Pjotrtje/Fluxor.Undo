#if NET7_0_OR_GREATER

using Fluxor.Undo.Tests.SmokeTestConfiguration;

using Microsoft.Extensions.DependencyInjection;

namespace Fluxor.Undo.Tests;

public class SmokeTests
{
    [Fact]
    public async Task ToDo()
    {
        await using var serviceProvider = GetServiceProvider();

        var store = serviceProvider.GetRequiredService<IStore>();
        await store.InitializeAsync();

        var testFlow = serviceProvider.GetRequiredService<TestFlow>();
        testFlow.Execute();
    }

    private static ServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();
        services
            .AddSingleton<TestFlow>()
            .AddFluxor(o => o.ScanAssemblies(typeof(SmokeTests).Assembly));

        return services.BuildServiceProvider();
    }

    private class TestFlow
    {
        private readonly IDispatcher _dispatcher;
        private readonly IState<UndoableCounterState> _state;

        public TestFlow(IDispatcher dispatcher, IState<UndoableCounterState> state)
        {
            _dispatcher = dispatcher;
            _state = state;
        }

        public void Execute()
        {
            Dispatch(
                new IncrementCounterAction(1),
                new IncrementCounterAction(10),
                new IncrementCounterAction(2),
                new UndoAction<UndoableCounterState>());

            _state.Value.Should().BeEquivalentTo(
                new UndoableCounterState
                {
                    Past = new[]
                    {
                        new CounterState(0),
                        new CounterState(1),
                    },
                    Present = new CounterState(11),
                    Future = new[]
                    {
                        new CounterState(13),
                    },
                });
        }

        private void Dispatch(params object[] actions)
        {
            foreach (var action in actions)
            {
                _dispatcher.Dispatch(action);
            }
        }
    }
}

#endif
