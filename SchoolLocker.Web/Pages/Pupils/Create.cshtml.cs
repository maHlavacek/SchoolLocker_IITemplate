using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolLocker.Core.Contracts;
using SchoolLocker.Core.Entities;
using SchoolLocker.Web.DataTransferObjects;

namespace SchoolLocker.Web.Pages.Pupils
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PupilDto Pupil { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Pupil pupil = new Pupil
            {
                FirstName = Pupil.FirstName,
                LastName = Pupil.LastName,
            };

            _unitOfWork.PupilRepository.Add(pupil);
            _unitOfWork.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}