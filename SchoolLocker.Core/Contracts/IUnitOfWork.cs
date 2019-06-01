using System;

namespace SchoolLocker.Core.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IPupilRepository PupilRepository { get; }
        IBookingRepository BookingRepository { get; }
        ILockerRepository LockerRepository { get; }

        int SaveChanges();


        void DeleteDatabase();

        void MigrateDatabase();
    }
}
