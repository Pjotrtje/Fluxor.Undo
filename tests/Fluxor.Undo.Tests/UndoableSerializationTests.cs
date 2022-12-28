namespace Fluxor.Undo.Tests;

public class UndoableSerializationTests
{
    [Fact]
    public void SystemTextJson_FirstSerializeAndThenDeserialize_Returns_Equivalent_State()
    {
        var originalState = GetUndoableIntState();
        var jsonString = System.Text.Json.JsonSerializer.Serialize(originalState);
        var deserializedState = System.Text.Json.JsonSerializer.Deserialize<UndoableIntState>(jsonString);

        originalState.Should().BeEquivalentTo(deserializedState);
    }

    [Fact]
    public void NewtonsoftJson_FirstSerializeAndThenDeserialize_Returns_Equivalent_State()
    {
        var originalState = GetUndoableIntState();
        var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(originalState);
        var deserializedState = Newtonsoft.Json.JsonConvert.DeserializeObject<UndoableIntState>(jsonString);

        originalState.Should().BeEquivalentTo(deserializedState);
    }

#if NET7_0_OR_GREATER

    private static UndoableIntState GetUndoableIntState()
        => new()
        {
            Past = new[] { 1, 2, 3 },
            Present = 55,
            Future = new[] { 10, 20, 30 },
        };

#else
    private static UndoableIntState GetUndoableIntState()
        => new(55)
        {
            Past = new[] { 1, 2, 3 },
            Future = new[] { 10, 20, 30 },
        };
#endif
}
