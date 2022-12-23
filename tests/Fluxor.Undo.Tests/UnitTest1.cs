using FluentAssertions;

namespace Fluxor.Undo.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
#if NET6_0
        var x = new UndoableIntState(0);

#else
        var x = new UndoableIntState { Present = 0, };
#endif

        x.Past.Should().BeEmpty();
        x.HasPast.Should().BeFalse();
        x.HasNoPast.Should().BeTrue();
        x.Present.Should().Be(0);
        x.Future.Should().BeEmpty();
        x.HasFuture.Should().BeFalse();
        x.HasNoFuture.Should().BeTrue();
    }
}

#if NET6_0
public record UndoableIntState(int Present)
    : Undoable<UndoableIntState, int>(Present);

#else
public record UndoableIntState : Undoable<UndoableIntState, int>;

#endif

//Json (de)serialize tests
//Smoke tests net6/net x attribute vs Feature<>
//Reducer tests
//Undoable tests

//Split net6/net7 unit tests/example?
//Rename example project
//How to name classes Generics?
