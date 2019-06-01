using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolLocker.Core.Contracts;
using SchoolLocker.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SchoolLocker.Web.Pages.Bookings
{
    public class LockerBookingModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Booking Booking { get; set; }

        public List<SelectListItem> Pupils { get; set; }

        public LockerBookingModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            Pupils = _unitOfWork
                    .PupilRepository
                    .GetAll()
                    .Select(p => new SelectListItem(
                        $"{p.FirstName} {p.LastName}"
                        , p.Id.ToString()))
                        .ToList();
        }
    }
}