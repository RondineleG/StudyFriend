﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudyFriend.Models;

namespace StudyFriend.Pages.Topics
{
    public class IndexModel : PageModel
    {
        private readonly StudyFriendContext _context;

        public IndexModel(StudyFriendContext context)
        {
            _context = context;
        }

        public IList<Topic> Topic { get;set; }

        public async Task OnGetAsync()
        {
            Topic = await _context.Topic.ToListAsync();
        }
    }
}
