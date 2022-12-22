using FluentAssertions;

namespace Fluxor.Undo.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var x = Undoable.Create(0);

        x.Past.Should().BeEmpty();
        x.TimeTravelInfo.HasPast.Should().BeFalse();
        x.TimeTravelInfo.HasNoPast.Should().BeTrue();
        x.Present.Should().Be(0);
        x.Future.Should().BeEmpty();
        x.TimeTravelInfo.HasFuture.Should().BeFalse();
        x.TimeTravelInfo.HasNoFuture.Should().BeTrue();
    }
}
