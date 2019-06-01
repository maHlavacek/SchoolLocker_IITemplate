using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolLocker.Core.Contracts;
using SchoolLocker.Core.Entities;
using SchoolLocker.Web.DataTransferObjects;

namespace SchoolLocker.Web.Pages.Pupils
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public PupilDto Pupil { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pupil = _unitOfWork.PupilRepository.GetById(id.Value);

            if (pupil == null)
            {
                return NotFound();
            }
            Pupil = new PupilDto
            {
                FirstName = pupil.FirstName,
                LastName = pupil.LastName,
                Id = pupil.Id,
                Version = pupil.Version
            };
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Pupil dbPupil = _unitOfWork.PupilRepository.GetById(Pupil.Id);
            dbPupil.FirstName = Pupil.FirstName;
            dbPupil.LastName = Pupil.LastName;
            dbPupil.Version = Pupil.Version;

            try
            {
                _unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PupilExists(Pupil.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PupilExists(int id)
        {
            return _unitOfWork.PupilRepository.GetById(id) != null;
        }
    }
}
