using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolLocker.Core.Contracts;
using SchoolLocker.Core.Entities;
using SchoolLocker.Web.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolLocker.Web.Pages.Bookings
{
    public class LockerBookingModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public int LockerNumber { get; set; }

        [BindProperty]
        public BookingDTO BookingDTO { get; set; }

        public List<SelectListItem> Pupils { get; set; }

        public LockerBookingModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int lockerNumber)
        {
            LockerNumber = lockerNumber;
            Pupils = _unitOfWork
                    .PupilRepository
                    .GetAll()
                    .Select(p => new SelectListItem(
                        $"{p.FirstName} {p.LastName}"
                        , p.Id.ToString()))
                        .ToList();
            BookingDTO = new BookingDTO
            {
                LockerNumber = lockerNumber,
                From = DateTime.Now.Date,
                PupilId = 0
            };

        }
        public IActionResult OnPostCreate()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
          //  _unitOfWork.BookingRepository.Add(BookingDTO);
           // _unitOfWork.SaveChanges();
            return RedirectToPage("/Index");
        }
    }
}