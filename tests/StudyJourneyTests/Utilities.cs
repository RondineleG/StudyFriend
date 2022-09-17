using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyJourney.Data;


namespace StudyJourneyTests
{
    class Utilities
    {
        public static DbContextOptions<StudyJourneyDbContext> TestDbContextOptions()
        {
            // Create a new service provider to create a new in-memory database.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance using an in-memory database and 
            // IServiceProvider that the context should resolve all of its 
            // services from.
            var builder = new DbContextOptionsBuilder<StudyJourneyDbContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
