using SchoolLocker.Core.DataTransferObjects;
using SchoolLocker.Core.Entities;

namespace SchoolLocker.Core.Contracts
{
    public interface ILockerRepository
    {
        SchoolLockerOverviewDto[] GetLockersOverview();

        int[] GetLockerNumbers();

        Locker GetByLockerNr(int lockerNr);

    }
}
