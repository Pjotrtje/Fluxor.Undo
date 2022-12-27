namespace Fluxor.Undo.Tests;

#if NET7_0_OR_GREATER

public class UndoableTests
{
    [Fact]
    public void UndoOne_WithPast_Moves_PresentToFuture_And_NewestPastToPresent()
    {
        var state = new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 3,
        };

        var newState = state.WithUndoOne();

        newState.Should().BeEquivalentTo(
            new UndoableIntState
            {
                Past = new[] { 0, 1 },
                Present = 2,
                Future = new[] { 3 },
            });
    }

    [Fact]
    public void UndoOne_WithPastWithFuture_Moves_PresentToFuture_And_NewestPastToPresent()
    {
        var state = new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 3,
            Future = new[] { 4, 5, 6 },
        };

        var newState = state.WithUndoOne();

        newState.Should().BeEquivalentTo(
            new UndoableIntState
            {
                Past = new[] { 0, 1 },
                Present = 2,
                Future = new[] { 3, 4, 5, 6 },
            });
    }

    [Fact]
    public void UndoOne_WithFuture_DoesNothing()
    {
        var state = new UndoableIntState
        {
            Present = 3,
            Future = new[] { 4, 5, 6 },
        };

        var newState = state.WithUndoOne();

        newState.Should().BeEquivalentTo(state);
    }

    [Fact]
    public void UndoAll_WithPast_Moves_PresentToFuture_OldestPastToPresent_And_AllOtherPastsToFuture()
    {
        var state = new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 3,
        };

        var newState = state.WithUndoAll();

        newState.Should().BeEquivalentTo(
            new UndoableIntState
            {
                Present = 0,
                Future = new[] { 1, 2, 3 },
            });
    }

    [Fact]
    public void UndoAll_WithPastWithFuture_Moves_PresentToFuture_OldestPastToPresent_And_AllOtherPastsToFuture()
    {
        var state = new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 3,
            Future = new[] { 4, 5, 6 },
        };

        var newState = state.WithUndoAll();

        newState.Should().BeEquivalentTo(
            new UndoableIntState
            {
                Present = 0,
                Future = new[] { 1, 2, 3, 4, 5, 6 },
            });
    }

    [Fact]
    public void UndoAll_WithFuture_DoesNothing()
    {
        var state = new UndoableIntState
        {
            Present = 3,
            Future = new[] { 4, 5, 6 },
        };

        var newState = state.WithUndoAll();

        newState.Should().BeEquivalentTo(state);
    }

    [Fact]
    public void RedoOne_WithPast_DoesNoting()
    {
        var state = new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 3,
        };

        var newState = state.WithRedoOne();

        newState.Should().BeEquivalentTo(state);
    }

    [Fact]
    public void RedoOne_WithPastWithFuture_Moves_PresentToPast_OldestFutureToPresent()
    {
        var state = new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 3,
            Future = new[] { 4, 5, 6 },
        };

        var newState = state.WithRedoOne();

        newState.Should().BeEquivalentTo(
            new UndoableIntState
            {
                Past = new[] { 0, 1, 2, 3 },
                Present = 4,
                Future = new[] { 5, 6 },
            });
    }

    [Fact]
    public void RedoOne_WithFuture_Moves_PresentToPast_OldestFutureToPresent()
    {
        var state = new UndoableIntState
        {
            Present = 3,
            Future = new[] { 4, 5, 6 },
        };

        var newState = state.WithRedoOne();

        newState.Should().BeEquivalentTo(
            new UndoableIntState
            {
                Past = new[] { 3 },
                Present = 4,
                Future = new[] { 5, 6 },
            });
    }

    [Fact]
    public void RedoAll_WithPast_DoesNoting()
    {
        var state = new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 3,
        };

        var newState = state.WithRedoAll();

        newState.Should().BeEquivalentTo(state);
    }

    [Fact]
    public void RedoAll_WithPastWithFuture_Moves_PresentToPast_NewestFutureToPresent_And_AllOtherFuturesToPast()
    {
        var state = new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 3,
            Future = new[] { 4, 5, 6 },
        };

        var newState = state.WithRedoAll();

        newState.Should().BeEquivalentTo(
            new UndoableIntState
            {
                Past = new[] { 0, 1, 2, 3, 4, 5 },
                Present = 6,
            });
    }

    [Fact]
    public void RedoAll_WithFuture_Moves_PresentToPast_NewestFutureToPresent_And_AllOtherFuturesToPast()
    {
        var state = new UndoableIntState
        {
            Present = 3,
            Future = new[] { 4, 5, 6 },
        };

        var newState = state.WithRedoAll();

        newState.Should().BeEquivalentTo(
            new UndoableIntState
            {
                Past = new[] { 3, 4, 5 },
                Present = 6,
            });
    }

    [Fact]
    public void WithNewPresent_PresentIsObject_MovesPresentToPast_AddsNewPresent_RemovedFuture()
    {
        var state = new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 3,
            Future = new[] { 4, 5, 6 },
        };

        var newState = state.WithNewPresent(1000);

        newState.Should().BeEquivalentTo(new UndoableIntState
        {
            Past = new[] { 0, 1, 2, 3 },
            Present = 1000,
        });
    }

    [Fact]
    public void WithNewPresent_PresentIsFunction_MovesPresentToPast_AddsNewPresent_RemovedFuture()
    {
        var state = new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 3,
            Future = new[] { 4, 5, 6 },
        };

        var newState = state.WithNewPresent(p => p + 100);

        newState.Should().BeEquivalentTo(new UndoableIntState
        {
            Past = new[] { 0, 1, 2, 3 },
            Present = 103,
        });
    }

    [Fact]
    public void WithNewPresent_PresentIsFunction_WhenNoChange_DoesNothing()
    {
        var state = new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 3,
            Future = new[] { 4, 5, 6 },
        };

        var newState = state.WithNewPresent(p => p);

        newState.Should().BeEquivalentTo(state);
    }

    [Fact]
    public void WithInlineEditedPresent_PresentIsObject_MovesPresentToPast_AddsNewPresent_RemovedFuture()
    {
        var state = new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 3,
            Future = new[] { 4, 5, 6 },
        };

        var newState = state.WithInlineEditedPresent(1000);

        newState.Should().BeEquivalentTo(new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 1000,
        });
    }

    [Fact]
    public void WithInlineEditedPresent_PresentIsFunction_MovesPresentToPast_AddsNewPresent_RemovedFuture()
    {
        var state = new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 3,
            Future = new[] { 4, 5, 6 },
        };

        var newState = state.WithInlineEditedPresent(p => p + 100);

        newState.Should().BeEquivalentTo(new UndoableIntState
        {
            Past = new[] { 0, 1, 2 },
            Present = 103,
        });
    }
}

#endif
