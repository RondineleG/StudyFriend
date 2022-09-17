using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(StudyJourney.Areas.Identity.IdentityHostingStartup))]
namespace StudyJourney.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}
