using SchoolLocker.Core.Entities;
using System;

namespace SchoolLocker.Core.Contracts
{
    public interface IBookingRepository
    {
        void AddRange(Booking[] bookings);
        void Add(Booking booking);

        Booking GetOverlappingBooking(Booking booking);
        Booking[] GetOverlappingBookings(int lockerNumber, DateTime @from, DateTime? to);
    }
}
