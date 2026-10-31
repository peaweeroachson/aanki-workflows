namespace Fixture.Tests;

public sealed class FixtureTests
{
    [Fact]
    public void GivenFixture_WhenTestsRun_ThenTheyPass()
    {
        // Arrange
        var expected = 4;

        // Act
        var actual = 2 + 2;

        // Assert
        Assert.Equal(expected, actual);
    }
}
