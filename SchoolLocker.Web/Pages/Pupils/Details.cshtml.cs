using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolLocker.Core.Contracts;
using SchoolLocker.Core.Entities;

namespace SchoolLocker.Web.Pages.Pupils
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
    }
}
