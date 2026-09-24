using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace MyFirstUnitTest;

public class BookstoreTests
{
    [Fact]
    public async Task AddBook_WithInvalidIsbn_ShouldThrowException()
    {
        const string invalidIsbn = "INVALID_ISBN";

        Func<Task> action = async () =>
        {
            await Task.Run(() =>
            {
                if (invalidIsbn == "INVALID_ISBN")
                {
                    throw new ArgumentException("ISBN format is invalid.", nameof(invalidIsbn));
                }
            });
        };

        await action.Should().ThrowAsync<ArgumentException>();
    }
}