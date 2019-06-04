using SchoolLocker.Core.Contracts;
using SchoolLocker.Core.DataTransferObjects;
using SchoolLocker.Core.Entities;
using System.Linq;

namespace SchoolLocker.Persistence
{
    internal class LockerRepository : ILockerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LockerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Locker GetById(int id)
        {
            return _dbContext
                   .Lockers
                   .Where(l => l.Id == id)
                   .SingleOrDefault();
        }

        public Locker GetByLockerNr(int lockerNr)
        {
            return _dbContext
                .Lockers
                .SingleOrDefault(l => l.Number == lockerNr);
        }

        public int[] GetLockerNumbers()
        {
            return _dbContext
                .Lockers
                .Select(locker => locker.Number)
                .OrderBy(nr => nr)
                .ToArray();
        }

        public SchoolLockerOverviewDto[] GetLockersOverview()
        {
            var groupedBookings = _dbContext.Bookings
                .OrderByDescending(b => b.From)
                .GroupBy(b => b.Locker)
                .Select(group => new { Locker = group.Key, Count = group.Count(), group.First().From, group.First().To })
                .ToList();

            return groupedBookings
                .Select(b => new SchoolLockerOverviewDto()
                {
                    Locker = b.Locker,
                    CountBookings = b.Count,
                    From = b.From,
                    To = b.To
                })
                .OrderBy(dto => dto.Locker.Number)
                .ToArray();
        }
    }
}