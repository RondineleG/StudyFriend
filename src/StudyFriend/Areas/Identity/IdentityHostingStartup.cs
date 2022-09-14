using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(StudyFriend.Areas.Identity.IdentityHostingStartup))]
namespace StudyFriend.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}