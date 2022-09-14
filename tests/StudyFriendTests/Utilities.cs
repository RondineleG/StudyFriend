using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyFriend.Data;


namespace StudyFriendTests
{
    class Utilities
    {
        public static DbContextOptions<StudyFriendContext> TestDbContextOptions()
        {
            // Create a new service provider to create a new in-memory database.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance using an in-memory database and 
            // IServiceProvider that the context should resolve all of its 
            // services from.
            var builder = new DbContextOptionsBuilder<StudyFriendContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
