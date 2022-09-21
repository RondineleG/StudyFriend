using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyJourney.Data;
using StudyJourney.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyJourney.Components
{
    public partial class TopicList
    {
        private readonly StudyJourneyDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string UserName;
        public TopicList(StudyJourneyDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<Topic> Topics { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }


        public async Task OnGetAsync()
        {
            ApplicationUser currUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            IQueryable<Topic> topics = from t in _context.Topic
                                       where t.UserId == currUser.Id
                                       select t;

            if (!string.IsNullOrEmpty(SearchString))
            {
                topics = topics.Where(s => s.Name.Contains(SearchString));
            }

            Topics = await topics.ToListAsync();
        }
    }
}
