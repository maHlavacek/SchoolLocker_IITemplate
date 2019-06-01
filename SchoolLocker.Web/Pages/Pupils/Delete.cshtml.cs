using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolLocker.Core.Contracts;
using SchoolLocker.Core.Entities;

namespace SchoolLocker.Web.Pages.Pupils
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Pupil Pupil { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pupil = _unitOfWork.PupilRepository.GetById(id.Value);

            if (Pupil == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pupil = _unitOfWork.PupilRepository.GetById(id.Value);

            if (Pupil != null)
            {
                _unitOfWork.PupilRepository.Delete(Pupil);
                _unitOfWork.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}
