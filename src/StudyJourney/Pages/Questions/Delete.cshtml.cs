﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
<<<<<<<< HEAD:src/StudyFriend/Pages/Questions/Delete.cshtml.cs
using StudyFriend.Data;
using StudyFriend.Models;
========
using StudyJourney.Data;
using StudyJourney.Models;
>>>>>>>> develop:src/StudyJourney/Pages/Questions/Delete.cshtml.cs
using System.Threading.Tasks;

namespace StudyJourney.Pages.Questions
{
    public class DeleteModel : PageModel
    {
        private readonly StudyFriendContext _context;

        public DeleteModel(StudyFriendContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Question
                .AsNoTracking()
                .Include(q => q.Topic)
                .FirstOrDefaultAsync(m => m.QuestionID == id);

            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Question
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.QuestionID == id);

            if (Question != null)
            {
                _context.Question.Remove(Question);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
