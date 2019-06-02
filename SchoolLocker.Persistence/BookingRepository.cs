using SchoolLocker.Core.Contracts;
using SchoolLocker.Core.Entities;
using System;
using System.Linq;

namespace SchoolLocker.Persistence
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void AddRange(Booking[] bookings)
        {
            _dbContext.Bookings.AddRange(bookings);
        }

        public void Add(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
        }

        public Booking GetOverlappingBooking(Booking booking)
        {
            DateTime end = booking.To ?? DateTime.MaxValue;

            return _dbContext.Bookings.FirstOrDefault(b =>
                b.Id != booking.Id && booking.LockerId == b.LockerId && (
                    (b.From >= booking.From && b.From <= end) //True if b.From is in range of booking
                    || (b.To >= booking.From && b.To <= end) //True if b.To is in range of booking
                    || (b.From < booking.From && b.To > end) //True if booking is completely inside b
                ));
        }

        public Booking[] GetOverlappingBookings(int lockerNumber, DateTime @from, DateTime? to)
        {
            if (to == null)
            {
                to = DateTime.MaxValue;
            }
            var bookings = _dbContext
                .Bookings
                .Where(b => b.LockerId == lockerNumber &&
                           (b.From >= @from && b.From <= to ||
                             b.To >= @from && (b.To != null && b.To <= to))
                ).ToArray();
            return bookings;
        }
    }
}