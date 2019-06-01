using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolLocker.Core.Contracts;
using SchoolLocker.Core.Entities;
using System.Collections.Generic;

namespace SchoolLocker.Web.Pages.Pupils
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<Pupil> Pupils { get; set; }

        public void OnGet()
        {
            Pupils = _unitOfWork.PupilRepository.GetAll();
        }
    }
}
