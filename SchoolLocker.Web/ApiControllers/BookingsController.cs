﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolLocker.Core.Contracts;
using SchoolLocker.Core.Entities;
using System;

namespace SchoolLocker.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Liefert alle überlappenden Buchungen
        /// </summary>
        /// <param name="lockerNumber"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <response code="200">Die Abfrage war erfolgreich.</response>
        /// <response code="404">Zu dieser Nummer gibt es keinen Spint!</response>
        // GET: api/Bookings/104;2013-07-29T21:58:39;2013-07-29T21:58:39
        [HttpGet("{lockerNumber, from, to}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Booking[]> GetOverlappingBookings(int lockerNumber, DateTime from, DateTime to)
        {
            if (_unitOfWork
                    .LockerRepository
                    .GetByLockerNr(lockerNumber) == null)
            {
                return NotFound();
            }

            var bookings = _unitOfWork.BookingRepository.GetOverlappingBookings(lockerNumber, from, to);
            return bookings;
        }

    }
}